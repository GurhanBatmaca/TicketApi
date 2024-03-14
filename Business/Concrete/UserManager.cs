using System.Text;
using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Shared;
using Shared.Helpers;

namespace Business;

public class UserManager : IUserService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IConfiguration _configuration;
    private readonly IHttpContextAccessor _accessor;
    private readonly IEmailSender _emailSender;
    public UserManager(IUnitOfWork? unitOfWork,IConfiguration configuration,IHttpContextAccessor accessor,IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
        _accessor = accessor;
        _emailSender = emailSender;
    }
    public string? Message { get ; set ; }

    public async Task<bool> Create(RegisterModel model)
    {
        if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.RePassword))
        {
            Message = "Zorunlu alan.";
            return false;
        }
        if(!CheckInput.IsValid(model.Email) || !CheckInput.IsValid(model.Password!))
        {
            Message = "Kullanılamaz karater (_-*-or-and-'-).";
            return false;
        }

        var checkEmail = await _unitOfWork!.Users.FindByEmail(model.Email);

        if(checkEmail is not null)
        {
            Message = "Bu e-posta adresi ile daha önce üye olunmuş.";
            return false;
        }

        var checkUserName = await _unitOfWork!.Users.FindByName(model.UserName);

        if(checkUserName is not null)
        {
            Message = "Bu kullanıcı adı daha önce alınmış.";
            return false;
        }

        var user = new AuthUser {
            UserName = model.UserName,
            FirstName = model.FirstName!,
            LastName = model.LastName!,
            Email = model.Email
        };

        if(!await _unitOfWork!.Users.Create(user,model.Password))
        {
            Message = "Şifre en az 6 karater uzunluğunda,büyük küçük harf ve alfanümerik olmalıdır.";
            return false;
        }

        Message = "Üyelik oluşturuldu.";
        await _unitOfWork.Users.AddToRole(user,"Customer");

        var token = await _unitOfWork!.Users.GenerateEmailConfirmationToken(user);
        var validToken = UrlConverter.EncodeUrl(token);

        var baseUrl = $"{_accessor.HttpContext!.Request.Scheme}://{_accessor.HttpContext!.Request.Host}{_accessor.HttpContext!.Request.PathBase}";

        await _emailSender.SendEmailAsync(user.Email!,"Üyelik onayı",$"Hesabınızı onaylamak için lütfen <a href='{baseUrl}/api/auth/confirmemail/{validToken}&{user.Id}'>linke</a> tıklayınız");

        return true;
    }

    public async Task<bool> ConfirmEmail(string token,string userId)
    {
        if(string.IsNullOrEmpty(token) || string.IsNullOrEmpty(userId))
        {
            Message = "Eksik url hatası.";
            return false;
        }

        var user = await _unitOfWork!.Users.FindById(userId);

        if(user is null)
        {
            Message = "User id hatası.";
            return false;
        }

        var validToken = UrlConverter.DecodeUrl(token);

        if(!await _unitOfWork.Users.ConfirmEmail(user,validToken))
        {
            Message = "Token hatası.";
            return false;
        }

        Message = "Üyelik onaylandı";
        return true;
    }

    public async Task<bool> FargotPassword(string email)
    {
        if(string.IsNullOrEmpty(email))
        {
            Message = "Eksik url hatası.";
            return false;
        }

        var user = await _unitOfWork!.Users.FindByEmail(email);

        if(user is null)
        {
            Message = "Kullanıcı bulunamadı.";
            return false;
        }

        var token = await _unitOfWork.Users.GeneratePasswordResetToken(user);
        var validToken = UrlConverter.EncodeUrl(token);

        await _emailSender.SendEmailAsync(user.Email!,"Şifre sıfırlama",$"Şifre sıfırlama kodu: {validToken}");

        Message = "Sıfırlama kodu e-posta adresine gönderildi.";
        return true;
    }
}
