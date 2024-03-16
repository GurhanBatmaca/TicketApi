using AutoMapper;
using Data;
using Entity;
using Shared;

namespace Business;

public class ActivityManager : IActivityService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IMapper? _mapper;
    public ActivityManager(IUnitOfWork? unitOfWork,IMapper? mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
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
        var activityList = await _unitOfWork!.Activities.GetAll();

        var activitys = activityList.Select(e => _mapper!.Map<ActivitySummaryDTO>(e));

        return activitys.ToList();

    }
    public async Task<List<ActivityDTO>> GetAllWithCategories()
    {
        var activityList = await _unitOfWork!.Activities.GetAllWithCategories();

        var activitys = activityList.Select(e => new ActivityDTO 
        {
            Name = e.Name,
            Url = e.Url,
            ImageUrl = e.ImageUrl,
            Categories = e.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList()
        });

        return activitys.ToList();
    }

    public async Task<ActivityDTO> GetById(int id)
    {
        var activity = await _unitOfWork!.Activities.GetById(id);

        var activityDTO = new ActivityDTO 
        {
            Name = activity!.Name,
            Url = activity.Url,
            ImageUrl = activity.ImageUrl,
            Categories = activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList()
        };

        return activityDTO;

    }
}
