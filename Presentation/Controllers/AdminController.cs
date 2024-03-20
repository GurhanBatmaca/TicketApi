using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/admin")]
public class AdminController: ControllerBase
{

    protected private ITicketService _ticketService;
    public AdminController(ITicketService ticketService)
    {
        _ticketService = ticketService;
    }

    [HttpPost]
    [Route("addticket")]
    public async Task<IActionResult> AddTicket([FromForm] TicketModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }
        if(await _ticketService.Create(model))
        {
            return Ok( _ticketService.SuccessResponse );
        }
        return BadRequest( _ticketService.ErrorResponse );
    }

    [HttpPut]
    [Route("updateticket")]
    public async Task<IActionResult> UpdateTicket([FromForm] TicketModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }
        if(await _ticketService.Update(model))
        {
            return Ok( _ticketService.SuccessResponse );
        }
        return BadRequest( _ticketService.ErrorResponse );
    }

    [HttpDelete]
    [Route("deleteticket/{id}")]
    public async Task<IActionResult> DeleteTicket(int id)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }
        if(await _ticketService.Delete(id))
        {
            return Ok( _ticketService.SuccessResponse );
        }
        return BadRequest( _ticketService.ErrorResponse );
    }

    [HttpGet]
    [Route("userlist")]
    public async Task<IActionResult> UserList(int page=1)
    {
        return Ok();
    }
}
