using AutoMapper;
using Data;
using Entity;
using Shared;

namespace Business;

public class AddressManager : IAddressService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IMapper? _mapper;
    public AddressManager(IUnitOfWork? unitOfWork,IMapper? mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    } 
    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public Task<bool> Create(Address entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Delete(Address entity)
    {
        throw new NotImplementedException();
    }

    public Task<bool> Update(Address entity)
    {
        throw new NotImplementedException();
    }
    public async Task<bool> GetAll(int page,int pageSize)
    {
        var addressList = await _unitOfWork!.Addresses.GetAll(page,pageSize);

        if(addressList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var addresses = addressList.Select(e => new AddressDTO {
            Title = e.Title,
            ImageUrl = e.ImageUrl,
            Url = e.Url,
            City = new CityDTO {
                Name = e.City!.Name,
                Url = e.City.Url,
                ImageUrl = e.City.ImageUrl,
                PlateNumber = e.City.PlateNumber
            }
        });

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Addresses.GetAllCount(),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalPages)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse 
        {
            Data = addresses,
            PageInfo = pageInfo
        };

        return true;
    }
    public async Task<bool> GetById(int id)
    {
        var address = await _unitOfWork!.Addresses.GetById(id);

        if(address is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Address id hatası."
            };
            return false;
        }

        var addressDTO = new AddressDTO {
            Title = address!.Title,
            ImageUrl = address.ImageUrl,
            Url = address.Url,
            City = new CityDTO {
                Name = address.City!.Name,
                Url = address.City.Url,
                ImageUrl = address.City.ImageUrl,
                PlateNumber = address.City.PlateNumber
            }
            
        };

        SuccessResponse = new SuccessResponse 
        {
            Data = addressDTO
        };

        return true;
    }

    public async Task<bool> GetCities()
    {
        var citiyList = await _unitOfWork!.Addresses.GetCities();

        if(citiyList is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var cities = citiyList.Select(e => _mapper!.Map<CityDTO>(e));

        SuccessResponse = new SuccessResponse 
        {
            Data = cities
        };

        return true;
    }

    public async Task<bool> GetCityById(int id)
    {
        var city = await _unitOfWork!.Addresses.GetCityById(id);

        if(city is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "City id hatası."
            };
            return false;
        }

        var cityDTO = _mapper!.Map<CityDTO>(city);

        SuccessResponse = new SuccessResponse 
        {
            Data = cityDTO
        };

        return true;

    }
}