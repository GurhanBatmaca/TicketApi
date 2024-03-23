using Microsoft.AspNetCore.Identity;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Shared;

namespace Data;

public class EfCoreUserRepository: IUserRepository
{
    private readonly SignInManager<AuthUser> _signInManager;
    private readonly UserManager<AuthUser> _userManager;
    private readonly IConfiguration _configuration;
    protected private StoreContext? _context;

    public EfCoreUserRepository(SignInManager<AuthUser> signInManager,UserManager<AuthUser> userManager,IConfiguration configuration,StoreContext? context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
        _context = context;
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

    public async Task<string> GeneratePasswordResetToken(AuthUser user)
    {
        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task<IList<string>> GetRoles(AuthUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<List<UserEntityDTO>> GetUserList(int page, int pageSize)
    {

        var users = _context!.Database.SqlQuery<UserEntityDTO>
        (
            $"Select U.Id,U.UserName,U.FirstName,U.LastName,U.Email,U.EmailConfirmed,U.JoinDate,( select R.Name + ' ' from Roles as R INNER JOIN UserRoles as UR on R.Id = UR.RoleId where UR.UserId = U.Id for xml path('') ) as Roles from Users as U order by U.JoinDate OFFSET {(page-1)*pageSize} ROWS FETCH NEXT {pageSize} ROWS ONLY"
        );

        return await users.ToListAsync();
    }

    public async Task<int> GetUserListCount()
    {

        var users = _context!.Database.SqlQuery<UserEntityDTO>($"Select U.Id from Users as U").AsQueryable();
      
        return await users.CountAsync();       
    }

    public async Task<bool> IsEmailConfirmed(AuthUser user)
    {
        return await _userManager.IsEmailConfirmedAsync(user);
    }

    public async Task<bool> ResetPassword(AuthUser user,string token,string password)
    {
        var result = await _userManager.ResetPasswordAsync(user,token,password);
        return result.Succeeded;
    }
}
