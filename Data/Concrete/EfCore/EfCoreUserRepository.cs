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

    public async Task<List<UserDTO>> GetUserList(int page, int pageSize)
    {

        var skip = (page-1)*pageSize;
        var skip_param = new SqlParameter("page_param", skip);
        var take_param = new SqlParameter("pageSize_param", pageSize);

        var users = _context!.Database.SqlQuery<UserDTO>($"SELECT u.Id,u.FirstName,u.LastName,u.JoinDate,u.UserName,u.Email,u.EmailConfirmed,u.PhoneNumber,r.Name as Role FROM Users as u inner join UserRoles as ur on u.Id = ur.UserId inner join Roles r on ur.RoleId = r.Id order by u.JoinDate OFFSET {skip_param} ROWS FETCH NEXT {take_param} ROWS ONLY ");

        return await users.ToListAsync();
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
