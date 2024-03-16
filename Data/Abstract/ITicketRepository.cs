using Entity;
using Shared;

namespace Data;

public interface ITicketRepository: IRepository<Ticket>
{
    Task<List<Ticket>> GetAll(int page,int pageSize);
    Task<int> GetAllCount();
    Task<List<Ticket>> GetFilterResult(FilterModel model,int page,int pageSize);
    Task<int> GetFilterResultCount(FilterModel model);
    Task<List<Ticket>> GetSearchResult(SearchModel model,int page,int pageSize);
    Task<int> GetSearchResultCount(SearchModel model);
    Task<Ticket?> GetById(int id);
}
