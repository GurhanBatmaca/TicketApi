using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreActivityRepository: EfCoreGenericRepository<Activity>,IActivityRepository
{
    public EfCoreActivityRepository(StoreContext context,IMapper? mapper):base(context)
    {
        _mapper = mapper;
    }
    protected IMapper? _mapper;
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<ActivitySummaryDTO>> GetAll()
    {
        var activityList = Context!.Activities.AsQueryable();

        if(activityList is null)
        {
            return [];
        }

        var activitys = activityList.Select(e => _mapper!.Map<ActivitySummaryDTO>(e));

        return await activitys.ToListAsync();
    }
    
    public async Task<List<ActivityDTO>> GetAllWithCategories()
    {
        var activityList = Context!.Activities
                                        .Include(e => e.ActivityCategories)
                                        .ThenInclude(e=>e.Category)
                                        .AsQueryable();

        if(activityList is null)
        {
            return [];
        }

        var activitys = activityList.Select(e => new ActivityDTO 
        {
            Name = e.Name,
            Url = e.Url,
            ImageUrl = e.ImageUrl,
            Categories = e.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList()
        });

        return await activitys.ToListAsync();
    }

    public async Task<ActivityDTO> GetById(int id)
    {
        var activity = await Context!.Activities
                                            .Include(e => e.ActivityCategories)
                                            .ThenInclude(e => e.Category)
                                            .FirstOrDefaultAsync(e=> e.Id == id);

        if (activity is null)
        {
            return new ActivityDTO();
        }

        var activityDTO = new ActivityDTO 
        {
            Name = activity.Name,
            Url = activity.Url,
            ImageUrl = activity.ImageUrl,
            Categories = activity.ActivityCategories.Select(i => _mapper!.Map<CategoryDTO>(i.Category)).ToList()
        };

        return activityDTO;
    }
}
