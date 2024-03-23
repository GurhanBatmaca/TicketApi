using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/activities")]
// [Authorize]
public class ActivityController: ControllerBase
{
    protected private IActivityService _activityService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public ActivityController(IActivityService activityService,IConfiguration configuration)
    {
        _activityService = activityService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All()
    {

        if(await _activityService.GetAll())
        {
            return Ok( _activityService.SuccessResponse );
        }

        return BadRequest( _activityService.ErrorResponse );

    }

    [HttpGet]
    [Route("allwithcategories")]
    public async Task<IActionResult> AllWithCategories()
    {

        if(await _activityService.GetAllWithCategories())
        {
            return Ok( _activityService.SuccessResponse );
        }

        return BadRequest( _activityService.ErrorResponse );

    }

    [HttpGet]
    [Route("activity/{id}")]
    public async Task<IActionResult> ActivityDetails(int id)
    {

        if(await _activityService.GetById(id))
        {
            return Ok( _activityService.SuccessResponse );
        }

        return BadRequest( _activityService.ErrorResponse );

    }
}
