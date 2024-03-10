using Business;
using Microsoft.AspNetCore.Mvc;

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
        var artors = await _artorService.GetAll();
        return Ok( new { Artors = artors } );
    }

    [HttpGet]
    [Route("allWithWorks")]
    public async Task<IActionResult> AllWithWorks()
    {
        var artors = await _artorService.GetAllWithWorks();
        return Ok( new { Artors = artors } );
    }

    [HttpGet]
    [Route("artor/{id}")]
    public async Task<IActionResult> ArtorDetails(int id)
    {
        var artor = await _artorService.GetById(id);

        if(!string.IsNullOrEmpty(artor!.Name))
        {
            return Ok( new { Artor = artor } );
        }
        else
        {
            return BadRequest( new { Error = "Artor id hatası" } );
        }
    }

}
