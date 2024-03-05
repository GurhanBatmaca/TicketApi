using Entity;
using Shared;

namespace Business;

public interface ITicketService: IService<Ticket>
{
    Task<List<TicketDTO>?> GetAllTickets(int page,int pageSize);
    Task<int> GetAllTicketsCount();
    Task<List<TicketDTO>> GetFilterResult(FilterModel model,int page,int pageSize);
    Task<int> GetFilterResultCount(FilterModel model);
    // Task<List<TicketDTO>> GetSearchResult(int page,int pageSize,string searcString,DateTime date);
    // Task<int> GetSearchResultCount(string searcString,DateTime date);
}
