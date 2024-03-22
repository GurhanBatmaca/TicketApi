using Shared;

namespace Business;

public interface IUserService: IService
{
    Task<bool> Create(RegisterModel model);
    Task<bool> ConfirmEmail(string userId,string token);
    Task<bool> GeneratePasswordResetToken(string email);
    Task<bool> ResetPassword(ResetPasswordModel model);
    Task<bool> GetUserList(int page,int pageSize);

}
