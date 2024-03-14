using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Shared;

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

    public async Task AddToRole(AuthUser user, string role)
    {
        await _userManager.AddToRoleAsync(user,role);
    }

    public async Task<bool> CheckPassword(AuthUser user,string password)
    {
        return await _userManager.CheckPasswordAsync(user,password);
    }

    public async Task<bool> ConfirmEmail(AuthUser user, string token)
    {
        var result = await _userManager.ConfirmEmailAsync(user,token);
        return result.Succeeded;
    }

    public async Task<bool> Create(AuthUser user,string password)
    {
        var result = await _userManager.CreateAsync(user,password);
        return result.Succeeded;
    }

    public async Task<AuthUser?> FindByEmail(string email)
    {
        return await _userManager!.FindByEmailAsync(email);
    }

    public async Task<AuthUser?> FindById(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<AuthUser?> FindByName(string userName)
    {
        return await _userManager!.FindByNameAsync(userName);
    }

    public async Task<string> GenerateEmailConfirmationToken(AuthUser user)
    {
        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
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
