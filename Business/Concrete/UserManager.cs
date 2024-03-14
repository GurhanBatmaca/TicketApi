using Data;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Business;

public class UserManager : IUserService
{
    protected private IUnitOfWork? _unitOfWork;
    protected private IConfiguration _configuration;
    public UserManager(IUnitOfWork? unitOfWork,IConfiguration configuration)
    {
        _unitOfWork = unitOfWork;
        _configuration = configuration;
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

        if(!await _unitOfWork!.Users.Create(model))
        {
            Message = "Şifre en az 6 karater uzunluğunda,büyük küçük harf ve alfanümerik olmalıdır.";
            return false;
        }

        Message = "Üyelik oluşturuldu.";
        return true;
    }
}
