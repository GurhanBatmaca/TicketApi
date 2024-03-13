using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/activities")]
[Authorize]
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
        var activities = await _activityService.GetAll();
        return Ok( new SuccessResponse { Data = activities } );
    }

    [HttpGet]
    [Route("allWithCategories")]
    public async Task<IActionResult> AllWithCategories()
    {
        var activities = await _activityService.GetAllWithCategories();
        return Ok( new SuccessResponse { Data = activities } );
    }

    [HttpGet]
    [Route("activity/{id}")]
    public async Task<IActionResult> ActivityDetails(int id)
    {
        var activity = await _activityService.GetById(id);

        if(string.IsNullOrEmpty(activity!.Name))
        {
            return BadRequest( new ErrorResponse { Error = "Activity id hatası" } );
        }
        return Ok( new SuccessResponse { Data = activity } );

    }
}
