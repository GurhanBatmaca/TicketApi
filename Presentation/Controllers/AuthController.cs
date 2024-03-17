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

        if(await _signService.Login(model))
        {
            return Ok( _signService.SuccessResponse );
        }

        return BadRequest( _signService.ErrorResponse );

        
    }
    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register([FromBody]RegisterModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }
        
        if(await _userService.Create(model))
        {
            return Ok( _userService.SuccessResponse );
        }
        
        return BadRequest( _userService.ErrorResponse );
        
    }

    [HttpGet]
    [Route("confirmemail/{token}&{userId}")]
    public async Task<IActionResult> ConfirmEmail(string token,string userId)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }

        if(await _userService.ConfirmEmail(token,userId))
        {
            return Ok( _userService.SuccessResponse );
        }
        
        return BadRequest( _userService.ErrorResponse );
        
    }

    [HttpGet]
    [Route("fargotpassword/{email}")]
    public async Task<IActionResult> FargotPassword([EmailAddress]string email)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }
        
        if(await _userService.GeneratePasswordResetToken(email))
        {
            return Ok( _userService.SuccessResponse );
        }
        
        return BadRequest( _userService.ErrorResponse );

    }

    [HttpPost]
    [Route("resetpassword")]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel model)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest( new ErrorResponse() );
        }

        if(await _userService.ResetPassword(model))
        {
            return Ok( _userService.SuccessResponse );
        }
        
        return BadRequest( _userService.ErrorResponse );
        
    }

}
