using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreAddressRepository: EfCoreGenericRepository<Address>,IAddressRepository
{
    public EfCoreAddressRepository(StoreContext context):base(context)
    {
    }
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<Address>> GetAll(int page,int PageSize)
    {

        var addressList = Context!.Addresses.Include(e=>e.City).AsQueryable();        
        return await addressList.Skip((page-1)*PageSize).Take(PageSize).ToListAsync();
        
    }

    public async Task<int> GetAllCount()
    {
        return await Context!.Addresses.CountAsync();
    }
    public async Task<Address?> GetById(int id)
    {
        return await Context!.Addresses.Include(e=>e.City).FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<City>> GetCities()
    {
        return await Context!.Cities.ToListAsync();
    }

    public async Task<City?> GetCityById(int id)
    {
        return await Context!.Cities.FirstOrDefaultAsync(e => e.Id == id);
    }
}
