using Entity;
using Shared;

namespace Business;

public interface ITicketService: IService<Ticket>
{
    Task<List<TicketSummaryDTO>?> GetAll(int page,int pageSize);
    Task<int> GetAllCount();

    Task<List<TicketSummaryDTO>> GetFilterResult(FilterModel model,int page,int pageSize);
    Task<int> GetFilterResultCount(FilterModel model);

    Task<List<TicketSummaryDTO>> GetSearchResult(SearchModel model,int page,int pageSize);
    Task<int> GetSearchResultCount(SearchModel model);

    Task<TicketDTO?> GetById(int id);
}