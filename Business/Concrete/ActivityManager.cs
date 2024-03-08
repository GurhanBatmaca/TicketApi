using Data;
using Entity;
using Shared;

namespace Business;

public class ActivityManager : IActivityService
{

    protected private IUnitOfWork? _unitOfWork;
    public ActivityManager(IUnitOfWork? unitOfWork)
    {
        _unitOfWork = unitOfWork;
    } 
    public string? Message { get; set; }

    public Task Create(Activity entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Activity entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Activity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ActivitySummaryDTO>> GetAll()
    {
        return await _unitOfWork!.Activities.GetAll();
    }
    public async Task<List<ActivityDTO>> GetAllWithCategories()
    {
        return await _unitOfWork!.Activities.GetAllWithCategories();
    }

    public async Task<ActivityDTO> GetById(int id)
    {
        return await _unitOfWork!.Activities.GetById(id);
    }
}
