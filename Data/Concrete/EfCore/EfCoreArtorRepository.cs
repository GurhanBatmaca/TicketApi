using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreArtorRepository: EfCoreGenericRepository<Artor>,IArtorRepository
{
    public EfCoreArtorRepository(StoreContext context):base(context)
    {
    }
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<Artor>> GetAll()
    {        
        return await Context!.Artors.ToListAsync();
    }

    public async Task<List<Artor>> GetAllWithWorks()
    {
        return await Context!.Artors
                                    .Include(e=>e.ArtorWorks)
                                    .ThenInclude(e=> e.Work)
                                    .ToListAsync();
    }

    public async Task<Artor?> GetById(int id)
    {
        return await Context!.Artors
                                    .Include(e=>e.ArtorWorks)
                                    .ThenInclude(e=> e.Work)
                                    .FirstOrDefaultAsync(e=> e.Id == id);

    }
}
