using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/admin")]
public class AdminController: ControllerBase
{

    [HttpPost]
    [Route("addticket")]
    public async Task<IActionResult> AddTicket([FromBody] TicketCreateModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }
        return Ok( "ok" );
    }
}
