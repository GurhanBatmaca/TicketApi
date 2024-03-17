using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Shared;

namespace Business;

public class SignManager : ISignService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IConfiguration _configuration;
    public SignManager(IUnitOfWork? unitOfWork,IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
    }
    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public async Task<bool> Login(LoginModel model)
    {
        if(!CheckInput.IsValid(model.Email!) || !CheckInput.IsValid(model.Password!))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Kullanılamaz karater hatası (_-*-or-and-'-)."
            };
            return false;
        }

        var user = await _unitOfWork!.Users.FindByEmail(model.Email!);

        if(user == null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "E-posta hatası,Bu e-posta ile kullanıcı bulunamadı."
            };
            return false;
        }

        if(!await _unitOfWork!.Users.IsEmailConfirmed(user))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Onaylanmamış hesap hatası."
            };
            return false;
        }

        var result = await _unitOfWork!.Users.CheckPassword(user,model.Password!);

        if(!result)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Yanlış şifre hatası."
            };
            return false;
        }

        var roles = await _unitOfWork!.Users.GetRoles(user);

        var claims = new List<Claim>()
        {
            new("Email",model.Password!),
            new(ClaimTypes.NameIdentifier,user.Id),           
            new(ClaimTypes.Name,user.UserName!)             
        };

        foreach (var role in roles)
        {
            claims.Add( new Claim(ClaimTypes.Role,role) );
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration!["JwtSettings:Key"]!));

        var token = new JwtSecurityToken(
            issuer: _configuration!["JwtSettings:Issuer"],
            audience: _configuration!["JwtSettings:Audince"],
            claims: claims,
            expires: DateTime.Now.AddDays(30),
            signingCredentials: new SigningCredentials(key,SecurityAlgorithms.HmacSha256)
        );

        var stringToken = new JwtSecurityTokenHandler().WriteToken(token);

        SuccessResponse = new SuccessResponse
        {
            Data = new TokenModel 
            {
                Token = stringToken,
                ExpireDate = token.ValidTo
            }
        };
        return true;
    }
}
