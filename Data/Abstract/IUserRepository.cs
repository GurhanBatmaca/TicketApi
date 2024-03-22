using Microsoft.AspNetCore.Identity;
using Shared;

namespace Data;

public interface IUserRepository
{
    Task<AuthUser?> FindByEmail(string email);
    Task<bool> CheckPassword(AuthUser user,string password);
    Task<IList<string>> GetRoles(AuthUser user);
    Task<bool> IsEmailConfirmed(AuthUser user);
    Task<bool> Create(AuthUser user,string password);
    Task<AuthUser?> FindByName(string userName);
    Task<string> GenerateEmailConfirmationToken(AuthUser user);
    Task AddToRole(AuthUser user,string role);
    Task<bool> ConfirmEmail(AuthUser user,string token);
    Task<AuthUser?> FindById(string id);
    Task<string> GeneratePasswordResetToken(AuthUser user);
    Task<bool> ResetPassword(AuthUser user,string token,string password);
    Task<List<UserEntity>> GetUserList(int page,int pageSize);
    Task<int> GetUserListCount();
}
