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
        model.Activity = UrlConverter.Convert(model.Activity!);
        model.Address = UrlConverter.Convert(model.Address!);
        model.Artor = UrlConverter.Convert(model.Artor!);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(
                                    e => e.Limit > 0 &&
                                    e.Activity!.Url.Contains(model.Activity) && 
                                    ( e.Address!.City.Contains(model.Address) || e.Address!.Title.Contains(model.Address) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor))
                                )
                                .AsQueryable();

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

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
        model.Activity = UrlConverter.Convert(model.Activity!);
        model.Address = UrlConverter.Convert(model.Address!);
        model.Artor = UrlConverter.Convert(model.Artor!);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(
                                    e => e.Limit > 0 &&
                                    e.Activity!.Url.Contains(model.Activity) && 
                                    ( e.Address!.City.Contains(model.Address) || e.Address!.Title.Contains(model.Address) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor))
                                )
                                .AsQueryable();

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<List<TicketDTO>> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        model.Query = UrlConverter.Convert(model.Query);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    (e.Url.Contains(model.Query) ||  e.Activity!.Url.Contains(model.Query) || e.Activity.ActivityCategories.Any(i=> i.Category!.Url.Contains(model.Query)) || e.Address!.City.Contains(model.Query) || e.Address!.Title.Contains(model.Query) || e.TicketArtors.Any(i => i.Artor!.Url.Contains(model.Query)))
                                )
                                .AsQueryable();

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

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

    public async Task<int> GetSearchResultCount(SearchModel model)
    {
        model.Query = UrlConverter.Convert(model.Query);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    (e.Url.Contains(model.Query) ||  e.Activity!.Url.Contains(model.Query) || e.Activity.ActivityCategories.Any(i=> i.Category!.Url.Contains(model.Query)) || e.Address!.City.Contains(model.Query) || e.Address!.Title.Contains(model.Query) || e.TicketArtors.Any(i => i.Artor!.Url.Contains(model.Query)))
                                )
                                .AsQueryable();

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }
}
