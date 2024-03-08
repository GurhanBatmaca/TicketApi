using Entity;
using Shared;

namespace Data;

public interface IAddressRepository: IRepository<Address>
{
    Task<List<AddressDTO>> GetAll(int page,int PageSize);
    Task<int> GetAllCount();
    Task<AddressDTO> GetById(int id);
}
