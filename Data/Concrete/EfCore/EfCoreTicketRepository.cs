using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreTicketRepository: EfCoreGenericRepository<Ticket>,ITicketRepository
{
    public EfCoreTicketRepository(StoreContext context):base(context)
    {
    }

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
            Address = e.Address!.Title,
            City = e.Address.City,
            Country = e.Address.Country,
            Activity = e.Activity!.Name,
            Artors = e.TicketArtors.Select(i=> i.Artor!.Name).ToList(),
        });

        return tickets is not null ? await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync() : [];
    }

    public async Task<int> GetAllTicketsCount()
    {
        return await Context!.Tickets.Where(e=> e.Limit > 0).CountAsync();
    }

    public async Task<List<TicketDTO>> GetTicketsByActivity(int page, int pageSize, string activity)
    {
        var ticketList = Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => e.Activity!.Url.Contains(activity))
                                .AsQueryable();


        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate.ToString("dd/MM/yyyy HH:mm"),
            Address = e.Address!.Title,
            City = e.Address.City,
            Country = e.Address.Country,
            Activity = e.Activity!.Name,
            Artors = e.TicketArtors.Select(i=> i.Artor!.Name).ToList(),
        });

        return tickets is not null ? await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync() : [];
    }

    public async Task<int> GetTicketsByActivityCount(string activity)
    {
        var ticketList = Context!.Tickets
                                .Where(e=> e.Limit > 0)
                                .Include(e=> e.Activity)
                                .Where(e => e.Activity!.Url.Contains(activity))
                                .AsQueryable();

        return await ticketList.CountAsync();
    }

    public async Task<List<TicketDTO>> GetSearchResult(int page, int pageSize, string searcString,DateTime date)
    {
        var ticketList = Context!.Tickets
                                // .Where(e=>  e.EventDate == date)
                                .Include(e=> e.Address)
                                .Include(e=> e.Activity)
                                .ThenInclude(e=> e!.ActivityCategories)
                                .ThenInclude(e=> e.Category)
                                .Include(e => e.TicketArtors)
                                .ThenInclude(e=> e.Artor)
                                .Where(e => e.Activity!.Url.Contains(searcString) && e.EventDate >= date)
                                .AsQueryable();


        var tickets = ticketList.Select(e => new TicketDTO {
            Name = e.Name,
            Price = e.Price,
            EventDate = e.EventDate.ToString("dd/MM/yyyy HH:mm"),
            Address = e.Address!.Title,
            City = e.Address.City,
            Country = e.Address.Country,
            Activity = e.Activity!.Name,
            Artors = e.TicketArtors.Select(i=> i.Artor!.Name).ToList(),
        });

        return tickets is not null ? await tickets.Skip((page-1)*pageSize).Take(pageSize).ToListAsync() : [];
    }

    public Task<int> GetSearchResultCount(string searcString,DateTime date)
    {
        throw new NotImplementedException();
    }
}
