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

    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public Task<bool> Create(Activity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Activity entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Activity entity)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> GetAll()
    {
        var activityList = await _unitOfWork!.Activities.GetAll();

        if(activityList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var activitys = activityList.Select(e => _mapper!.Map<ActivitySummaryDTO>(e));

        SuccessResponse = new SuccessResponse 
        {
            Data = activitys
        };

        return true;

    }
    public async Task<bool> GetAllWithCategories()
    {
        var activityList = await _unitOfWork!.Activities.GetAllWithCategories();

        if(activityList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var activitys = activityList.Select(e => new ActivityDTO 
        {
            Name = e.Name,
            Url = e.Url,
            ImageUrl = e.ImageUrl,
            Categories = e.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList()
        });


        SuccessResponse = new SuccessResponse 
        {
            Data = activitys
        };

        return true;
    }

    public async Task<bool> GetById(int id)
    {
        var activity = await _unitOfWork!.Activities.GetById(id);

        if(activity is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Activity id hatası."
            };
            return false;
        }

        var activityDTO = new ActivityDTO 
        {
            Name = activity!.Name,
            Url = activity.Url,
            ImageUrl = activity.ImageUrl,
            Categories = activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList()
        };

        SuccessResponse = new SuccessResponse 
        {
            Data = activityDTO
        };

        return true;
    }
}
