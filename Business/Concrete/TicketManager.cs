using AutoMapper;
using Data;
using Entity;
using Shared;
using Shared.Helpers;

namespace Business;

public class TicketManager : ITicketService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IMapper? _mapper;
    public TicketManager(IUnitOfWork? unitOfWork,IMapper? mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public async Task<bool> GetAll(int page, int pageSize)
    {
        var ticketList =  await _unitOfWork!.Tickets.GetAll(page,pageSize);

        if(ticketList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
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

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Tickets.GetAllCount(),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalItems)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = tickets,
            PageInfo = pageInfo
        };

        return true;
    }

    public async Task<bool> GetFilterResult(FilterModel model,int page,int pageSize)
    {
        model.Activity = UrlConverter.Edit(model.Activity!);
        model.Address = UrlConverter.Edit(model.Address!);
        model.Artor = UrlConverter.Edit(model.Artor!);

        var ticketList = await _unitOfWork!.Tickets.GetFilterResult(model,page,pageSize);

        if(ticketList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
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

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Tickets.GetFilterResultCount(model),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalItems)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = tickets,
            PageInfo = pageInfo
        };

        return true;
    }

    public async Task<bool> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        model.Query = UrlConverter.Edit(model.Query);
        var ticketList = await _unitOfWork!.Tickets.GetSearchResult(model,page,pageSize);

        if(ticketList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
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

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Tickets.GetSearchResultCount(model),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalItems)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = tickets,
            PageInfo = pageInfo
        };

        return true;
    }

    public async Task<bool> GetById(int id)
    {
        var ticket = await _unitOfWork!.Tickets.GetById(id);

        if(ticket is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Ticket id hatası."
            };
            return false;
        }

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
                Categories = ticket.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
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
            Artors = ticket.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList()
        };

        SuccessResponse = new SuccessResponse 
        {
            Data = ticketDTO
        };

        return true;
    }

}