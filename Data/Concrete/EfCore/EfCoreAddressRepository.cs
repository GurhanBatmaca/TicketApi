﻿using Entity;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace Data;

public class EfCoreAddressRepository: EfCoreGenericRepository<Address>,IAddressRepository
{
    public EfCoreAddressRepository(StoreContext context):base(context)
    {
    }
    protected StoreContext? Context => _context as StoreContext;

    public async Task<List<AddressDTO>> GetAll(int page,int PageSize)
    {
        var addressList = Context!.Addresses.Include(e=>e.City).AsQueryable();

        if(addressList is null)
        {
            return [];
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

        return await addresses.Skip((page-1)*PageSize).Take(PageSize).ToListAsync();
    }

    public async Task<int> GetAllCount()
    {
        return await Context!.Addresses.CountAsync();
    }
    public async Task<AddressDTO> GetById(int id)
    {
        var address = await Context!.Addresses.Include(e=>e.City).FirstOrDefaultAsync(e => e.Id == id);

        if(address is null)
        {
            return new AddressDTO();
        }

        var addressDTO = new AddressDTO {
            Title = address.Title,
            ImageUrl = address.ImageUrl,
            Url = address.Url,
            City = new CityDTO {
                Name = address.City!.Name,
                Url = address.City.Url,
                ImageUrl = address.City.ImageUrl,
                PlateNumber = address.City.PlateNumber
            }
            
        };

        return addressDTO;
    }

    public async Task<List<CityDTO>> GetCities()
    {
        var citiyList = Context!.Cities.AsQueryable();

        if(citiyList is null)
        {
            return [];
        }

        var cities = citiyList.Select(e => _mapper!.Map<CityDTO>(e));

        return await cities.ToListAsync();
    }

    public async Task<CityDTO> GetCityById(int id)
    {
        var address = await Context!.Addresses
                                    .Include(e=> e.City)
                                    .FirstOrDefaultAsync(e => e.City!.Id == id);

        if(address is null)
        {
            return new CityDTO();
        }

        var cityDTO = _mapper!.Map<CityDTO>(address.City);

        return cityDTO;
    }
}
