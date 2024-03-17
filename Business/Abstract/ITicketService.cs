using Entity;
using Shared;

namespace Business;

public interface ITicketService: IService
{
    Task<bool> GetAll(int page,int pageSize);

    Task<bool> GetFilterResult(FilterModel model,int page,int pageSize);

    Task<bool> GetSearchResult(SearchModel model,int page,int pageSize);

    Task<bool> GetById(int id);
    Task<bool> Create(TicketCreateModel model);
}