using Shared;

namespace Business;

public interface IUserService
{
    string? Message {get;set;}
    Task<bool> Create(RegisterModel model);
    Task<bool> ConfirmEmail(string userId,string token);
    Task<bool> GeneratePasswordResetToken(string email);
    Task<bool> ResetPassword(ResetPasswordModel model);

}
