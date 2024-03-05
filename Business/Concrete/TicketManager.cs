using Data;
using Entity;
using Shared;
using Shared.Helpers;

namespace Business;

public class TicketManager : ITicketService
{
    protected private IUnitOfWork? _unitOfWork;
    public TicketManager(IUnitOfWork? unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public string? Message { get; set; }

    public Task Create(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public Task<List<Ticket>?> GetAll()
    {
        throw new NotImplementedException();
    }

    public async Task<List<TicketDTO>?> GetAllTickets(int page, int pageSize)
    {
        return await _unitOfWork!.Tickets.GetAllTickets(page,pageSize);
    }

    public async Task<int> GetAllTicketsCount()
    {
        return await _unitOfWork!.Tickets.GetAllTicketsCount();
    }

    public Task<Ticket?> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TicketDTO>> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        return await _unitOfWork!.Tickets.GetSearchResult(model,page,pageSize);
    }

    public async Task<int> GetSearchResultCount(SearchModel model)
    {
        return await _unitOfWork!.Tickets.GetSearchResultCount(model);
    }

    public async Task<List<TicketDTO>> GetFilterResult(FilterModel model,int page,int pageSize)
    {
        return await _unitOfWork!.Tickets.GetFilterResult(model,page,pageSize);
    }

    public async Task<int> GetFilterResultCount(FilterModel model)
    {
        return await _unitOfWork!.Tickets.GetFilterResultCount(model);
    }

    public Task Update(Ticket entity)
    {
        throw new NotImplementedException();
    }
}
