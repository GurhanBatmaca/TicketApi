using Microsoft.AspNetCore.Mvc;

namespace Presentation;

[ApiController]
[Route("api/")]
public class TicketController: ControllerBase
{
    [HttpGet]
    [Route("get")]
    public IActionResult GetTest()
    {
        return Ok("Okey");
    }
}