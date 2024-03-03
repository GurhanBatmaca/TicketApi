using Entity;
using Shared;

namespace Data;

public interface ITicketRepository: IRepository<Ticket>
{
    Task<List<TicketDTO>> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
    Task<List<TicketDTO>> GetTicketsByActivity(int page,int pageSize,string activity);
    Task<int> GetTicketsByActivityCount(string activity);
    Task<List<TicketDTO>> GetSearchResult(int page,int pageSize,string searcString,DateTime date);
    Task<int> GetSearchResultCount(string searcString,DateTime date);
}
