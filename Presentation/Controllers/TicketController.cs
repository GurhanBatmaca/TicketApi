using Business;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shared;
namespace Presentation;

[ApiController]
[Route("api/tickets")]
[Authorize]
public class TicketController: ControllerBase
{
    protected private ITicketService _ticketService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public TicketController(ITicketService ticketService,IConfiguration configuration)
    {
        _ticketService = ticketService;
        _configuration = configuration;
    }

    [HttpGet]
    [Route("all")]
    public async Task<IActionResult> All(int page=1)
    {
        
        if(await _ticketService.GetAll(page,PageSize))
        {
            return Ok( _ticketService.SuccessResponse );
        }

        return BadRequest( _ticketService.ErrorResponse );
    }

    [HttpGet]
    [Route("filter")]
    public async Task<IActionResult> Filter([FromQuery] FilterModel model,int page=1)
    {

        if(await _ticketService.GetFilterResult(model,page,PageSize))
        {
            return Ok( _ticketService.SuccessResponse );
        }

        return BadRequest( _ticketService.ErrorResponse );
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> Search([FromQuery] SearchModel model,int page=1)
    {

        if(await _ticketService.GetSearchResult(model,page,PageSize))
        {
            return Ok( _ticketService.SuccessResponse );
        }

        return BadRequest( _ticketService.ErrorResponse );
    }

    [HttpGet]
    [Route("ticket/{id}")]
    public async Task<IActionResult> TicketDetails(int id)
    {
        
        if(await _ticketService.GetById(id))
        {
            return Ok( _ticketService.SuccessResponse );
        }

        return BadRequest( _ticketService.ErrorResponse );
    }

}