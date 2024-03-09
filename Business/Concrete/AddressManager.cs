using Data;
using Entity;
using Shared;

namespace Business;

public class AddressManager : IAddressService
{
    protected private IUnitOfWork? _unitOfWork;
    public AddressManager(IUnitOfWork? unitOfWork)
    {
        _unitOfWork = unitOfWork;
    } 
    public string? Message { get; set; }

    public Task Create(Address entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Address entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(Address entity)
    {
        throw new NotImplementedException();
    }
    public async Task<List<AddressDTO>> GetAll(int page,int PageSize)
    {
        return await _unitOfWork!.Addresses.GetAll(page,PageSize);
    }

    public async Task<int> GetAllCount()
    {
        return await _unitOfWork!.Addresses.GetAllCount();
    }
    public async Task<AddressDTO> GetById(int id)
    {
        return await _unitOfWork!.Addresses.GetById(id);
    }

    public async Task<List<CityDTO>> GetCities()
    {
        return await _unitOfWork!.Addresses.GetCities();
    }

    public async Task<CityDTO> GetCityById(int id)
    {
        return await _unitOfWork!.Addresses.GetCityById(id);
    }
}