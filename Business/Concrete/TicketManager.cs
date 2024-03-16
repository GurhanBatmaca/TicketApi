using Data;
using Entity;
using Shared;
using Shared.Helpers;

namespace Business;

public class TicketManager : ITicketService
{
    protected private IUnitOfWork? _unitOfWork;
    public TicketManager(IUnitOfWork? unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public string? Message { get; set; }

    public Task Create(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TicketSummaryDTO>?> GetAll(int page, int pageSize)
    {
        var ticketList =  await _unitOfWork!.Tickets.GetAll(page,pageSize);

        if(ticketList is null)
        {
            return [];
        }

        var tickets = ticketList.Select(e => new TicketSummaryDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = e.Activity!.Name,
            Address = e.Address!.Title,
            City = e.Address.City!.Name
        });

        return tickets.ToList();
    }

    public async Task<int> GetAllCount()
    {
        return await _unitOfWork!.Tickets.GetAllCount();
    }

    public async Task<List<TicketSummaryDTO>> GetFilterResult(FilterModel model,int page,int pageSize)
    {
        var ticketList = await _unitOfWork!.Tickets.GetFilterResult(model,page,pageSize);

         var tickets = ticketList.Select(e => new TicketSummaryDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = e.Activity!.Name,
            Address = e.Address!.Title,
            City = e.Address.City!.Name
        });

        return tickets.ToList();
    }

    public async Task<int> GetFilterResultCount(FilterModel model)
    {
        return await _unitOfWork!.Tickets.GetFilterResultCount(model);
    }

    public async Task<List<TicketSummaryDTO>> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        var ticketList = await _unitOfWork!.Tickets.GetSearchResult(model,page,pageSize);

        var tickets = ticketList.Select(e => new TicketSummaryDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = e.Activity!.Name,
            Address = e.Address!.Title,
            City = e.Address.City!.Name
        });

        return tickets.ToList();
    }

    public async Task<int> GetSearchResultCount(SearchModel model)
    {
        return await _unitOfWork!.Tickets.GetSearchResultCount(model);
    }

    public async Task<TicketDTO?> GetById(int id)
    {
        var ticket = await _unitOfWork!.Tickets.GetById(id);

        var ticketDTO = new TicketDTO
        {
            Name = ticket!.Name,
            Price = ticket.Price,
            Url = ticket.Url,
            EventDate = ticket.EventDate,
            ImageUrl = ticket.ImageUrl,
            Activity = new ActivityDTO {
                Name = ticket.Activity!.Name,
                Url = ticket.Activity.Url,
                ImageUrl = ticket.Activity.ImageUrl,
                // Categories = ticket.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = new AddressDTO {
                Title = ticket.Address!.Title,
                ImageUrl = ticket.Address.ImageUrl,
                City = new CityDTO {
                    Name = ticket.Address.City!.Name,
                    ImageUrl = ticket.Address.City!.ImageUrl,
                    Url = ticket.Address.City!.Url
                }
            },
            // Artors = ticket.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList()
        };

        return ticketDTO;
    }
}