

using Microsoft.EntityFrameworkCore;

namespace Data;

public class EfCoreGenericRepository<T> : IRepository<T>
    where T : class
{
    protected readonly StoreContext? _context;
    public EfCoreGenericRepository(StoreContext? context)
    {
        _context = context;
    }
    public async Task Create(T entity)
    {
        await _context!.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Delete(T entity)
    {
        _context!.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
    }

    public async Task<List<T>?> GetAll()
    {
        return await _context!.Set<T>().ToListAsync();
    }

    public async Task<T?> GetById(int id)
    {
        return await _context!.Set<T>().FindAsync(id);
    }

    public async Task Update(T entity)
    {
        _context!.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}
