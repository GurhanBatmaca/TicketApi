using AutoMapper;
using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreAddressRepository: EfCoreGenericRepository<Address>,IAddressRepository
{
    public EfCoreAddressRepository(StoreContext context,IMapper? mapper):base(context)
    {
        _mapper = mapper;
    }
    protected IMapper? _mapper;
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<AddressDTO>> GetAll(int page,int PageSize)
    {
        var addressList = Context!.Addresses.AsQueryable();

        if(addressList is null)
        {
            return [];
        }

        var addresses = addressList.Select(e => _mapper!.Map<AddressDTO>(e));

        return await addresses.Skip((page-1)*PageSize).Take(PageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await Context!.Addresses.CountAsync();
    }
    public async Task<AddressDTO> GetById(int id)
    {
        var address = await Context!.Addresses.FirstOrDefaultAsync(e => e.Id == id);

        if(address is null)
        {
            return new AddressDTO();
        }

        var addressDTO = _mapper!.Map<AddressDTO>(address);

        return addressDTO;
    }

}
