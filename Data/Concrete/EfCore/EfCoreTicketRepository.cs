using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Helpers;

namespace Data;

public class EfCoreTicketRepository: EfCoreGenericRepository<Ticket>,ITicketRepository
{
  
    public EfCoreTicketRepository(StoreContext context,IMapper? mapper):base(context)
    {
        _mapper = mapper;
    }
    protected IMapper? _mapper;
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<TicketDTO>> GetAllTickets(int page,int pageSize)
    {
        var ticketList = Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .AsQueryable();

        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate.ToString("dd/MM/yyyy HH:mm"),
            Activity = new ActivityDTO {
                Name = e.Activity!.Name,
                Url = e.Activity.Url,
                ImageUrl = e.Activity.ImageUrl,
                Categories = e.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = _mapper!.Map<AddressDTO>(e.Address),
            Artors = e.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList(),
        });

        return tickets is not null ? await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync() : [];
    }

    public async Task<int> GetAllTicketsCount()
    {
        return await Context!.Tickets.Where(e=> e.Limit > 0).CountAsync();
    }

    public async Task<List<TicketDTO>> GetFilterResult(FilterModel model, int page, int pageSize)
    {
        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(
                                    e => e.Limit > 0 && 
                                    e.Activity!.Url.Contains(UrlConverter.Convert(model.Activity!)) && 
                                    ( e.Address!.City.Contains(UrlConverter.Convert(model.Address!)) || e.Address!.Title.Contains(UrlConverter.Convert(model.Address!)) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(UrlConverter.Convert(model.Artor!)))
                                )
                                .AsQueryable();


        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate.ToString("dd/MM/yyyy HH:mm"),
            Activity = new ActivityDTO {
                Name = e.Activity!.Name,
                Url = e.Activity.Url,
                ImageUrl = e.Activity.ImageUrl,
                Categories = e.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = _mapper!.Map<AddressDTO>(e.Address),
            Artors = e.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList(),
        });

        return tickets is not null ? await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync() : [];
    }

    public async Task<int> GetFilterResultCount(FilterModel model)
    {
        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(
                                    e => e.Limit > 0 && 
                                    e.Activity!.Url.Contains(UrlConverter.Convert(model.Activity!)) && 
                                    ( e.Address!.City.Contains(UrlConverter.Convert(model.Address!)) || e.Address!.Title.Contains(UrlConverter.Convert(model.Address!)) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(UrlConverter.Convert(model.Artor!)))
                                )
                                .AsQueryable();

        return await ticketList.CountAsync();
    }

    // public async Task<List<TicketDTO>> GetSearchResult(int page, int pageSize, string searcString,DateTime date)
    // {
    //     var ticketList = Context!.Tickets
    //                             // .Where(e=>  e.EventDate == date)
    //                             .Include(e=> e.Address)
    //                             .Include(e=> e.Activity)
    //                             .ThenInclude(e=> e!.ActivityCategories)
    //                             .ThenInclude(e=> e.Category)
    //                             .Include(e => e.TicketArtors)
    //                             .ThenInclude(e=> e.Artor)
    //                             .Where(e => e.Activity!.Url.Contains(searcString) && e.EventDate >= date)
    //                             .AsQueryable();


    //     var tickets = ticketList.Select(e => new TicketDTO {
    //         Name = e.Name,
    //         Price = e.Price,
    //         EventDate = e.EventDate.ToString("dd/MM/yyyy HH:mm"),
    //         Address = e.Address!.Title,
    //         City = e.Address.City,
    //         Country = e.Address.Country,
    //         Activity = e.Activity!.Name,
    //         Artors = e.TicketArtors.Select(i=> i.Artor!.Name).ToList(),
    //     });

    //     return tickets is not null ? await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync() : [];
    // }

    // public Task<int> GetSearchResultCount(string searcString,DateTime date)
    // {
    //     throw new NotImplementedException();
    // }
}
