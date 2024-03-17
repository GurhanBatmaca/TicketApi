using Entity;
using Shared;

namespace Business;

public interface IAddressService: IService
{
    Task<bool> GetAll(int page,int pageSize);
    Task<bool> GetById(int id);
    Task<bool> GetCities();
    Task<bool> GetCityById(int id);
}
