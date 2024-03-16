using Microsoft.AspNetCore.Mvc;

namespace Presentation;

[ApiController]
[Route("api/admin")]
public class AdminController: ControllerBase
{

    [HttpPost]
    [Route("addticket")]
    public async Task<IActionResult> AddTicket()
    {
        return Ok();
    }
}
