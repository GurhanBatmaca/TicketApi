using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Data;

public class IdentityContext: IdentityDbContext<AuthUser>
{
    public IdentityContext(DbContextOptions<IdentityContext> options):base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<AuthUser>().Property(e => e.JoinDate).HasDefaultValueSql("getdate()");
        builder.Entity<IdentityUserLogin<string>>().HasKey(e=> new { e.LoginProvider,e.ProviderKey});
        builder.Entity<IdentityUserRole<string>>().HasKey(e=> new { e.UserId,e.RoleId});
        builder.Entity<IdentityUserToken<string>>().HasKey(e=> new { e.UserId,e.LoginProvider,e.Name});
    }
}
