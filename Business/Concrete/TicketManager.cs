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

    public Task Update(Ticket entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<TicketSummaryDTO>?> GetAll(int page, int pageSize)
    {
        return await _unitOfWork!.Tickets.GetAll(page,pageSize);
    }

    public async Task<int> GetAllCount()
    {
        return await _unitOfWork!.Tickets.GetAllCount();
    }

    public async Task<List<TicketDTO>?> GetAllWithDetails(int page, int pageSize)
    {
        return await _unitOfWork!.Tickets.GetAllWithDetails(page,pageSize);
    }

    public async Task<int> GetAllWithDetailsCount()
    {
        return await _unitOfWork!.Tickets.GetAllWithDetailsCount();
    }

    public async Task<List<TicketSummaryDTO>> GetFilterResult(FilterModel model,int page,int pageSize)
    {
        return await _unitOfWork!.Tickets.GetFilterResult(model,page,pageSize);
    }

    public async Task<int> GetFilterResultCount(FilterModel model)
    {
        return await _unitOfWork!.Tickets.GetFilterResultCount(model);
    }

    public async Task<List<TicketDTO>> GetFilterResultWithDetails(FilterModel model, int page, int pageSize)
    {
        return await _unitOfWork!.Tickets.GetFilterResultWithDetails(model,page,pageSize);
    }

    public async Task<int> GetFilterResultWithDetailsCount(FilterModel model)
    {
        return await _unitOfWork!.Tickets.GetFilterResultWithDetailsCount(model);
    }

    public async Task<List<TicketSummaryDTO>> GetSearchResult(SearchModel model,int page,int pageSize)
    {
        return await _unitOfWork!.Tickets.GetSearchResult(model,page,pageSize);
    }

    public async Task<int> GetSearchResultCount(SearchModel model)
    {
        return await _unitOfWork!.Tickets.GetSearchResultCount(model);
    }

    public async Task<List<TicketDTO>> GetSearchResultWithDetails(SearchModel model, int page, int pageSize)
    {
        return await _unitOfWork!.Tickets.GetSearchResultWithDetails(model,page,pageSize);
    }

    public async Task<int> GetSearchResultWithDetailsCount(SearchModel model)
    {
        return await _unitOfWork!.Tickets.GetSearchResultWithDetailsCount(model);
    }

    public async Task<TicketDTO?> GetById(int id)
    {
        return await _unitOfWork!.Tickets.GetById(id);
    }
}