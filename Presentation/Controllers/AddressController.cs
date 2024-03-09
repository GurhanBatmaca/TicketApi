using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/addresses")]
public class AddressController: ControllerBase
{

    protected private IAddressService _addressService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public AddressController(IAddressService addressService,IConfiguration configuration)
    {
        _addressService = addressService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All(int page=1)
    {
        var addresses = await _addressService.GetAll(page,PageSize);
        var totalItems = await _addressService.GetAllCount();      
        var pageInfo = new PageInfo {TotalItems=totalItems,ItemPerPage=PageSize,CurrentPage=page,};

        if(page > pageInfo.TotalPages)
        {
            return BadRequest( new { Error = "İndex hatası,Liste boyutu aşıldı." } );
        }

        return Ok( new { Addresses = addresses, PageInfo = pageInfo } );
    }

    [HttpGet]
    [Route("address/{id}")]
    public async Task<IActionResult> AddressDetails(int id)
    {
        var address = await _addressService.GetById(id);

        if(!string.IsNullOrEmpty(address!.Title))
        {
            return Ok( new { Address = address } );
        }
        else
        {
            return BadRequest( new { Error = "Address id hatası" } );
        }
    }

    [HttpGet]
    [Route("cities")]
    public async Task<IActionResult> Cities()
    {
        var cities = await _addressService.GetCities();
        return Ok( new { Cities = cities } );

    }

    [HttpGet]
    [Route("cities/city/{id}")]
    public async Task<IActionResult> CityDetails(int id)
    {
        var city = await _addressService.GetCityById(id);

        if(!string.IsNullOrEmpty(city!.Name))
        {
            return Ok( new { City = city } );
        }
        else
        {
            return BadRequest( new { Error = "City id hatası" } );
        }
    }

}