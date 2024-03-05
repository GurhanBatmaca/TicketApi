using Entity;
using Shared;

namespace Data;

public interface ITicketRepository: IRepository<Ticket>
{
    Task<List<TicketDTO>> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
    Task<List<TicketDTO>> GetFilterResult(FilterModel model,int page,int pageSize);
    Task<int> GetFilterResultCount(FilterModel model);
    // Task<List<TicketDTO>> GetSearchResult(int page,int pageSize,string searcString,DateTime date);
    // Task<int> GetSearchResultCount(string searcString,DateTime date);
}
