namespace Data;

public interface IUserRepository
{
    Task<AuthUser?> FindByEmail(string email);
    Task<bool> CheckPassword(AuthUser user,string password);
    Task<IList<string>> GetRoles(AuthUser user);
    Task<bool> IsEmailConfirmed(AuthUser user);
}
