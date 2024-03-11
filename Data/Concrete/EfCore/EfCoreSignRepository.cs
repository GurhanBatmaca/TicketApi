
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Data;

public class EfCoreSignRepository : ISignRepository
{

    private readonly SignInManager<AuthUser> _signInManager;
    private readonly UserManager<AuthUser> _userManager;
    private readonly IConfiguration _configuration;

    public EfCoreSignRepository(SignInManager<AuthUser> signInManager,UserManager<AuthUser> userManager,IConfiguration configuration)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _configuration = configuration;
    }

}
