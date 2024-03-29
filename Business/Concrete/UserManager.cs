using Data;
using Microsoft.AspNetCore.Http;
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
    public SuccessResponse? SuccessResponse { get ; set ; }
    public ErrorResponse? ErrorResponse { get ; set ; }

    public async Task<bool> Create(RegisterModel model)
    {
        if(string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.FirstName) || string.IsNullOrEmpty(model.LastName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.RePassword))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Zorunlu alan hatası."
            };
            return false;
        }
        if(!CheckInput.IsValid(model.Email) || !CheckInput.IsValid(model.Password!))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Kullanılamaz karater hatası (_-*-or-and-'-)."
            };
            return false;
        }

        // username türkçe karater araması yapılacak

        var checkEmail = await _unitOfWork!.Users.FindByEmail(model.Email);

        if(checkEmail is not null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "E-posta hatası,e-posta zaten kayıtlı."
            };
            return false;
        }

        var checkUserName = await _unitOfWork!.Users.FindByName(model.UserName);

        if(checkUserName is not null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Username hatası,Username zaten kayıtlı."
            };
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
            ErrorResponse = new ErrorResponse 
            {
                Error = "Şifre hatası,Şifre en az 6 karater uzunluğunda,büyük küçük harf ve alfanümerik olmalıdır."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse
        {
            Message = "Üyelik oluşturuldu."           
        };

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
            ErrorResponse = new ErrorResponse 
            {
                Error = "Eksik url hatası."
            };
            return false;
        }

        var user = await _unitOfWork!.Users.FindById(userId);

        if(user is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "User id hatası."
            };
            return false;
        }

        var validToken = UrlConverter.DecodeUrl(token);

        if(!await _unitOfWork.Users.ConfirmEmail(user,validToken))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Token id hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse
        {
            Message = "Üyelik onaylandı."           
        };

        return true;
    }

    public async Task<bool> GeneratePasswordResetToken(string email)
    {
        if(string.IsNullOrEmpty(email))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Eksik url hatası."
            };
            return false;
        }

        var user = await _unitOfWork!.Users.FindByEmail(email);

        if(user is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "E-posta hatası,kullanıcı bulunamadı."
            };
            return false;
        }

        var token = await _unitOfWork.Users.GeneratePasswordResetToken(user);
        var validToken = UrlConverter.EncodeUrl(token);

        await _emailSender.SendEmailAsync(user.Email!,"Şifre sıfırlama",$"Şifre sıfırlama kodu: {validToken}");

        SuccessResponse = new SuccessResponse
        {
            Message = "Sıfırlama kodu e-posta adresine gönderildi."           
        };

        return true;
    }

    public async Task<bool> ResetPassword(ResetPasswordModel model)
    {
        if(string.IsNullOrEmpty(model.Token) || string.IsNullOrEmpty(model.Email) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.RePassword))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Zorunlu alan hatası."
            };
            return false;
        }

        var user = await _unitOfWork!.Users.FindByEmail(model.Email!);
        if(user is null)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "E-posta hatası,kullanıcı bulunamadı."
            };
            return false;
        }

        var validToken = UrlConverter.DecodeUrl(model.Token!);

        if(!await _unitOfWork.Users.ResetPassword(user!,validToken,model.Password!))
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Şifre hatası,Şifre en az 6 karater uzunluğunda,büyük küçük harf ve alfanümerik olmalıdır."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse
        {
            Message = "Şifre değiştirldi."           
        };

        return true;

    }

    public async Task<bool> GetUserList(int page, int pageSize)
    {
        var usersList = await _unitOfWork!.Users.GetUserList(page,pageSize);

        if(usersList is null)
        {
            ErrorResponse = new ErrorResponse
            {
                Error = "Boş liste hatası."
            };
            return false;
        }

        var users = usersList.Select(e => new UserDTO {
            Id = e.Id,
            UserName = e.UserName,
            FirstName = e.FirstName,
            LastName = e.LastName,
            JoinDate = e.JoinDate.ToString("yyyy/dd/MM HH:mm:ss"),
            Email = e.Email,
            EmailConfirmed = e.EmailConfirmed,
            Roles = e.Roles.Trim().Split(" ").ToList()
        });

        var pageInfo = new PageInfo 
        {
            TotalItems = await _unitOfWork.Users.GetUserListCount(),
            ItemPerPage = pageSize,
            CurrentPage = page
        };

        if(page > pageInfo.TotalPages)
        {
            ErrorResponse = new ErrorResponse 
            {
                Error = "Index hatası."
            };
            return false;
        }

        SuccessResponse = new SuccessResponse
        {
            Data = users,
            PageInfo = pageInfo
        };
        return true;
    }
}
