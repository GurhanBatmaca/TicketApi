using System.ComponentModel.DataAnnotations;
using Business;
using Microsoft.AspNetCore.Mvc;
using Shared;

namespace Presentation;

[ApiController]
[Route("api/auth")]
public class AuthController: ControllerBase
{

    protected private ISignService _signService;
    protected private IUserService _userService;
    protected private IConfiguration _configuration;
    public int PageSize => Int32.Parse(_configuration["PageSize"]!);
    public AuthController(ISignService signService,IUserService userService,IConfiguration configuration)
    {
        _signService = signService;
        _userService = userService;
        _configuration = configuration;
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login([FromBody]LoginModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse { Error = "Lütfen eksik alanları doldurunuz." } );
        }
        if(!await _signService.Login(model))
        {
            return BadRequest( new ErrorResponse { Error = _signService.Message! } );
        }
        
        return Ok( new SuccessResponse { Data = _signService.TokenModel! } );
        
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]RegisterModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse { Error = "Lütfen eksik alanları doldurunuz." } );
        }
        
        if(!await _userService.Create(model))
        {
            return BadRequest( new ErrorResponse { Error = _userService.Message! } );
        }
        
        return Ok( new SuccessResponse { Message = _userService.Message! } );
        
    }

    [HttpGet]
    [Route("confirmemail/{token}&{userId}")]
    public async Task<IActionResult> ConfirmEmail(string token,string userId)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse { Error = "Lütfen eksik alanları doldurunuz." } );
        }
        
        if(!await _userService.ConfirmEmail(token,userId))
        {
            return BadRequest( new ErrorResponse { Error = _userService.Message! } );
        }
        
        return Ok( new SuccessResponse { Message = _userService.Message! } );
    }

    [HttpGet]
    [Route("fargotpassword/{email}")]
    public async Task<IActionResult> FargotPassword([EmailAddress]string email)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse { Error = "Lütfen eksik alanları doldurunuz." } );
        }
        
        if(!await _userService.FargotPassword(email))
        {
            return BadRequest( new ErrorResponse { Error = _userService.Message! } );
        }
        
        return Ok( new SuccessResponse { Message = _userService.Message! } );
    }

}
