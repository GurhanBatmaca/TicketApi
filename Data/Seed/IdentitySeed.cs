using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data;

public static class IdentitySeed
{
    public async static void Seed(IApplicationBuilder app, IConfiguration configuration)
    {
        var userManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<UserManager<AuthUser>>();
        var roleManager = app.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();


        var roles = configuration.GetSection("Identity:Roles").GetChildren().Select(x => x.Value).ToArray();

        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role!))
            {
                await roleManager.CreateAsync(new IdentityRole(role!));
            }
        }

        var users = configuration.GetSection("Identity:Users");

        foreach (var section in users.GetChildren())
        {
            var username = section.GetValue<string>("username");
            var password = section.GetValue<string>("password");
            var email = section.GetValue<string>("email");
            var role = section.GetValue<string>("role");
            var firstName = section.GetValue<string>("firstName");
            var lastName = section.GetValue<string>("lastName");

            if (await userManager.FindByNameAsync(username!) == null)
            {
                var user = new AuthUser()
                {
                    UserName = username,
                    Email = email,
                    FirstName = firstName!,
                    LastName = lastName!,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, password!);

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role!);
                }
            }
        }
    }    
}