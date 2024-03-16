using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreActivityRepository: EfCoreGenericRepository<Activity>,IActivityRepository
{
    public EfCoreActivityRepository(StoreContext context):base(context)
    {
    }
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<Activity>> GetAll()
    {
        return await Context!.Activities.ToListAsync();
    }
    
    public async Task<List<Activity>> GetAllWithCategories()
    {
        return await Context!.Activities
                                    .Include(e => e.ActivityCategories)
                                    .ThenInclude(e=>e.Category)
                                    .ToListAsync();

    }

    public async Task<Activity?> GetById(int id)
    {
        return await Context!.Activities
                                    .Include(e => e.ActivityCategories)
                                    .ThenInclude(e => e.Category)
                                    .FirstOrDefaultAsync(e=> e.Id == id);
    }
}
