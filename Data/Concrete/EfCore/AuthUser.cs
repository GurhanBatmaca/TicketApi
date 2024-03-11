using Microsoft.AspNetCore.Identity;

namespace Data;

public class AuthUser: IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public DateTime JoinDate { get; set; }
}
