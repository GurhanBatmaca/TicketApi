using Entity;
using Shared;

namespace Data;

public interface ITicketRepository: IRepository<Ticket>
{
    Task<List<TicketDTO>> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
    Task<List<TicketDTO>> GetFilterResult(FilterModel model,int page,int pageSize);
    Task<int> GetFilterResultCount(FilterModel model);
    Task<List<TicketDTO>> GetSearchResult(SearchModel model,int page,int pageSize);
    Task<int> GetSearchResultCount(SearchModel model);
}
