using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{

    protected private ISignService _signService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public AuthController(ISignService signService,IConfiguration configuration)
    {
        _signService = signService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]LoginModel model)
    {
        if(!await _signService.Login(model))
        {
            return BadRequest( new ErrorResponse { Error = _signService.Message! } );
        }
        
        return Ok( new SuccessResponse { Data = _signService.TokenModel! } );
        
    }

}
