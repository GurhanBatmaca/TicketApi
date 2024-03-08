using Microsoft.EntityFrameworkCore;

namespace Data;

public class EfCoreGenericRepository<T> : IRepository<T>
    where T : class
{
    protected readonly DbContext? _context;
    public EfCoreGenericRepository(DbContext? context)
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
    public async Task Update(T entity)
    {
        _context!.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}