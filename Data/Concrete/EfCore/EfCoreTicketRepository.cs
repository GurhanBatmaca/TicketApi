using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;
using Shared.Helpers;

namespace Data;

public class EfCoreTicketRepository: EfCoreGenericRepository<Ticket>,ITicketRepository
{
  
    public EfCoreTicketRepository(StoreContext context):base(context)
    {
    }
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<Ticket>> GetAll(int page,int pageSize)
    {
        var tickets = Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .ThenInclude(e=> e!.City)
                                .Include(e=> e.Activity)
                                .AsQueryable();

        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await Context!.Tickets.Where(e=> e.Limit > 0).CountAsync();
    }
 
    public async Task<List<Ticket>> GetFilterResult(FilterModel model, int page, int pageSize)
    {

        var tickets = Context!.Tickets
                                .Include(e=> e.Address)
                                .ThenInclude(e => e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => 
                                    (e.EventDate >= model.Date && e.Limit > 0 ) &&  
                                    e.Activity!.Url.Contains(model.Activity!) && 
                                    ( e.Address!.City!.Url.Contains(model.Address!) || e.Address!.Title.Contains(model.Address!) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor!))
                                )
                                .AsQueryable();

        if(model.Price > 0)
        {
            tickets =  tickets.Where(e => e.Price <= model.Price);
        }
       
        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetFilterResultCount(FilterModel model)
    {

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
                                    e.Activity!.Url.Contains(model.Activity!) && 
                                    ( e.Address!.City!.Url.Contains(model.Address!) || e.Address!.Title.Contains(model.Address!) ) &&
                                    e.TicketArtors.Any(i=> i.Artor!.Url.Contains(model.Artor!))
                                )
                                .AsQueryable();

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<List<Ticket>> GetSearchResult(SearchModel model,int page,int pageSize)
    {

        var tickets = Context!.Tickets
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

        if(model.Price > 0)
        {
            tickets =  tickets.Where(e => e.Price <= model.Price);
        }
       
        return await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync();
    }

    public async Task<int> GetSearchResultCount(SearchModel model)
    {

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

        if(model.Price > 0)
        {
            ticketList =  ticketList.Where(e => e.Price <= model.Price);
        }

        return await ticketList.CountAsync();
    }

    public async Task<Ticket?> GetById(int id)
    {
        return await Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .ThenInclude(e=>e!.City)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .FirstOrDefaultAsync(e=> e.Id == id);

    }
}