using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Data;

public class EfCoreUserRepository: IUserRepository
{
    private readonly SignInManager<AuthUser> _signInManager;
    private readonly UserManager<AuthUser> _userManager;
    private readonly IConfiguration _configuration;

    public EfCoreUserRepository(SignInManager<AuthUser> signInManager,UserManager<AuthUser> userManager,IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task<bool> CheckPassword(AuthUser user,string password)
    {
        return await _userManager.CheckPasswordAsync(user,password);
    }

    public async Task<AuthUser?> FindByEmail(string email)
    {
        return await _userManager!.FindByEmailAsync(email);
    }

    public async Task<IList<string>> GetRoles(AuthUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<bool> IsEmailConfirmed(AuthUser user)
    {
        return await _userManager.IsEmailConfirmedAsync(user);
    }
}
