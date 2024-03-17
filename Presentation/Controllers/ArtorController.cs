using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/artors")]
public class ArtorController: ControllerBase
{

    protected private IArtorService _artorService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public ArtorController(IArtorService artorService,IConfiguration configuration)
    {
        _artorService = artorService;
        _configuration = configuration;
    }


    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All()
    {
        if(await _artorService.GetAll())
        {
            return Ok( _artorService.SuccessResponse );
        }

        return BadRequest( _artorService.ErrorResponse );
    }

    [HttpGet]
    [Route("allWithWorks")]
    public async Task<IActionResult> AllWithWorks()
    {
        if(await _artorService.GetAllWithWorks())
        {
            return Ok( _artorService.SuccessResponse );
        }

        return BadRequest( _artorService.ErrorResponse );
    }

    [HttpGet]
    [Route("artor/{id}")]
    public async Task<IActionResult> ArtorDetails(int id)
    {
        if(await _artorService.GetById(id))
        {
            return Ok( _artorService.SuccessResponse );
        }

        return BadRequest( _artorService.ErrorResponse );
    }

}
