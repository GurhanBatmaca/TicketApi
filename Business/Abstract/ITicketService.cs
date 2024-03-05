using Entity;
using Shared;

namespace Business;

public interface ITicketService: IService<Ticket>
{
    Task<List<TicketDTO>?> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
    Task<List<TicketDTO>> GetFilterResult(FilterModel model,int page,int pageSize);
    Task<int> GetFilterResultCount(FilterModel model);
    Task<List<TicketDTO>> GetSearchResult(SearchModel model,int page,int pageSize);
    Task<int> GetSearchResultCount(SearchModel model);
}
