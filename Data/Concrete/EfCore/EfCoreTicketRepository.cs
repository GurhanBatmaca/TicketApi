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

    public async Task<List<TicketSummaryDTO>> GetAll(int page,int pageSize)
    {
        var ticketList = Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .AsQueryable();

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

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await Context!.Tickets.Where(e=> e.Limit > 0).CountAsync();
    }

    public async Task<List<TicketDTO>> GetAllWithDetails(int page, int pageSize)
    {
        var ticketList = Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .ThenInclude(e=> e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .AsQueryable();

        if(ticketList is null)
        {
            return [];
        }

        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            Url = e.Url,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = new ActivityDTO {
                Name = e.Activity!.Name,
                Url = e.Activity.Url,
                ImageUrl = e.Activity.ImageUrl,
                Categories = e.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = new AddressDTO {
                Title = e.Address!.Title,
                ImageUrl = e.Address.ImageUrl,
                City = new CityDTO {
                    Name = e.Address.City!.Name,
                    ImageUrl = e.Address.City!.ImageUrl,
                    Url = e.Address.City!.Url
                }
            
            },
            Artors = e.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList(),
        });

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllWithDetailsCount()
    {
        return await Context!.Tickets.Where(e=> e.Limit > 0).CountAsync();
    }

    public async Task<List<TicketSummaryDTO>> GetFilterResult(FilterModel model, int page, int pageSize)
    {
        model.Activity = UrlConverter.Convert(model.Activity!);
        model.Address = UrlConverter.Convert(model.Address!);
        model.Artor = UrlConverter.Convert(model.Artor!);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e => e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) &&  
                                    e.Activity!.Url.Contains(model.Activity) && 
                                    ( e.Address!.City!.Url.Contains(model.Address) || e.Address!.Title.Contains(model.Address) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return [];
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
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

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetFilterResultCount(FilterModel model)
    {
        model.Activity = UrlConverter.Convert(model.Activity!);
        model.Address = UrlConverter.Convert(model.Address!);
        model.Artor = UrlConverter.Convert(model.Artor!);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e => e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) &&  
                                    e.Activity!.Url.Contains(model.Activity) && 
                                    ( e.Address!.City!.Url.Contains(model.Address) || e.Address!.Title.Contains(model.Address) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return 0;
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<List<TicketDTO>> GetFilterResultWithDetails(FilterModel model, int page, int pageSize)
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
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    e.Activity!.Url.Contains(model.Activity) && 
                                    ( e.Address!.City!.Url.Contains(model.Address) || e.Address!.Title.Contains(model.Address) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return [];
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            Url = e.Url,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = new ActivityDTO {
                Name = e.Activity!.Name,
                Url = e.Activity.Url,
                ImageUrl = e.Activity.ImageUrl,
                Categories = e.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = new AddressDTO {
                Title = e.Address!.Title,
                ImageUrl = e.Address.ImageUrl,
                City = new CityDTO {
                    Name = e.Address.City!.Name,
                    ImageUrl = e.Address.City!.ImageUrl,
                    Url = e.Address.City!.Url
                }
            },
            Artors = e.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList(),
        });

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetFilterResultWithDetailsCount(FilterModel model)
    {
        model.Activity = UrlConverter.Convert(model.Activity!);
        model.Address = UrlConverter.Convert(model.Address!);
        model.Artor = UrlConverter.Convert(model.Artor!);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e=>e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    e.Activity!.Url.Contains(model.Activity) && 
                                    ( e.Address!.City!.Url.Contains(model.Address) || e.Address!.Title.Contains(model.Address) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return 0;
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<List<TicketSummaryDTO>> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        model.Query = UrlConverter.Convert(model.Query);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e=>e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    (e.Url.Contains(model.Query) ||  e.Activity!.Url.Contains(model.Query) || e.Activity.ActivityCategories.Any(i=> i.Category!.Url.Contains(model.Query)) || e.Address!.City!.Name.Contains(model.Query) || e.Address!.Title.Contains(model.Query) || e.Address!.City.Url.Contains(model.Query) || e.TicketArtors.Any(i => i.Artor!.Url.Contains(model.Query)))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return [];
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
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

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
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
                                    (e.Url.Contains(model.Query) ||  e.Activity!.Url.Contains(model.Query) || e.Activity.ActivityCategories.Any(i=> i.Category!.Url.Contains(model.Query)) || e.Address!.City!.Name.Contains(model.Query) || e.Address!.Title.Contains(model.Query) || e.Address!.City.Url.Contains(model.Query) || e.TicketArtors.Any(i => i.Artor!.Url.Contains(model.Query)))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return 0;
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<List<TicketDTO>> GetSearchResultWithDetails(SearchModel model, int page, int pageSize)
    {
        model.Query = UrlConverter.Convert(model.Query);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e=>e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    (e.Url.Contains(model.Query) ||  e.Activity!.Url.Contains(model.Query) || e.Activity.ActivityCategories.Any(i=> i.Category!.Url.Contains(model.Query)) || e.Address!.City!.Name.Contains(model.Query) || e.Address!.Title.Contains(model.Query) || e.Address!.City.Url.Contains(model.Query) || e.TicketArtors.Any(i => i.Artor!.Url.Contains(model.Query)))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return [];
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            Url = e.Url,
            EventDate = e.EventDate,
            ImageUrl = e.ImageUrl,
            Activity = new ActivityDTO {
                Name = e.Activity!.Name,
                Url = e.Activity.Url,
                ImageUrl = e.Activity.ImageUrl,
                Categories = e.Activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList() 
            },
            Address = new AddressDTO {
                Title = e.Address!.Title,
                ImageUrl = e.Address.ImageUrl,
                City = new CityDTO {
                    Name = e.Address.City!.Name,
                    ImageUrl = e.Address.City!.ImageUrl,
                    Url = e.Address.City!.Url
                }
            },
            Artors = e.TicketArtors.Select( i=> _mapper!.Map<ArtorDTO>(i.Artor)).ToList(),
        });

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetSearchResultWithDetailsCount(SearchModel model)
    {
        model.Query = UrlConverter.Convert(model.Query);

        var ticketList = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e=>e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) && 
                                    (e.Url.Contains(model.Query) ||  e.Activity!.Url.Contains(model.Query) || e.Activity.ActivityCategories.Any(i=> i.Category!.Url.Contains(model.Query)) || e.Address!.City!.Name.Contains(model.Query) || e.Address!.Title.Contains(model.Query) || e.Address!.City.Url.Contains(model.Query) || e.TicketArtors.Any(i => i.Artor!.Url.Contains(model.Query)))
                                )
                                .AsQueryable();

        if(ticketList is null)
        {
            return 0;
        }

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<TicketDTO?> GetById(int id)
    {
        var ticket = await Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .ThenInclude(e=>e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .FirstOrDefaultAsync(e=> e.Id == id);

        if(ticket is null)
        {       
            return new TicketDTO();
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

        return ticketDTO;

    }
}