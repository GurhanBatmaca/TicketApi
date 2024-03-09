using Entity;
using Shared;

namespace Business;

public interface IAddressService: IService<Address>
{
    Task<List<AddressDTO>> GetAll(int page,int PageSize);
    Task<int> GetAllCount();
    Task<AddressDTO> GetById(int id);
    Task<List<CityDTO>> GetCities();
    Task<CityDTO> GetCityById(int id);
}
