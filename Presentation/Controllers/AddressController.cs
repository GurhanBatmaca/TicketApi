using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/addresses")]
[Authorize]
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
        if(await _addressService.GetAll(page,PageSize))
        {
            return Ok( _addressService.SuccessResponse );
        }

        return BadRequest( _addressService.ErrorResponse );
    }

    [HttpGet]
    [Route("address/{id}")]
    public async Task<IActionResult> AddressDetails(int id)
    {
        if(await _addressService.GetById(id))
        {
            return Ok( _addressService.SuccessResponse );
        }

        return BadRequest( _addressService.ErrorResponse );

    }

    [HttpGet]
    [Route("cities")]
    public async Task<IActionResult> Cities()
    {
        if(await _addressService.GetCities())
        {
            return Ok( _addressService.SuccessResponse );
        }

        return BadRequest( _addressService.ErrorResponse );

    }

    [HttpGet]
    [Route("cities/city/{id}")]
    public async Task<IActionResult> CityDetails(int id)
    {
        if(await _addressService.GetCityById(id))
        {
            return Ok( _addressService.SuccessResponse );
        }

        return BadRequest( _addressService.ErrorResponse );
    }

}