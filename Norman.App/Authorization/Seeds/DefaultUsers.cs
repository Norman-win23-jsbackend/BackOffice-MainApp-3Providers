using Microsoft.AspNetCore.Identity;
using Norman.App.Authorization.Contants;
using Norman.App.Authorization.Enum;
using Norman.Common.Services.Models;

namespace Norman.App.Authorization.Seeds;

public static class DefaultUsers
{
    public static async Task SeedBasicUserAsync(UserManager<ApplicationUser> userManager)
    {
        var defaultUser = new ApplicationUser
        {
            UserName = Users.Basic.UserName,
            Email = Users.Basic.Email,
            EmailConfirmed = Users.Basic.EmailConfirmed,
            FirstName = Users.Basic.UserName,
            LastName = Users.Basic.UserName,
        };
        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(defaultUser, Users.Basic.Password);
            await userManager.AddToRoleAsync(defaultUser, Roles.User.ToString());
        }
    }
    public static async Task SeedAdminUserAsync(UserManager<ApplicationUser> userManager)
    {
        var defaultUser = new ApplicationUser
        {
            UserName = Users.Admin.UserName,
            Email = Users.Admin.Email,
            EmailConfirmed = Users.Admin.EmailConfirmed,
            FirstName = Users.Admin.UserName,
            LastName = Users.Admin.UserName,
        };
        var user = await userManager.FindByEmailAsync(defaultUser.Email);
        if (user == null)
        {
            await userManager.CreateAsync(defaultUser, Users.Admin.Password);
            var roles = new List<string> { Roles.Admin.ToString() };
            await userManager.AddToRolesAsync(defaultUser, roles.ToArray());
        }
    }
}
