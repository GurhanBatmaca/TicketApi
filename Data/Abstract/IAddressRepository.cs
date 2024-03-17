using Entity;
using Shared;

namespace Data;

public interface IAddressRepository: IRepository<Address>
{
    Task<List<Address>> GetAll(int page,int pageSize);
    Task<int> GetAllCount();
    Task<Address?> GetById(int id);
    Task<List<City>> GetCities();
    Task<City?> GetCityById(int id);
}