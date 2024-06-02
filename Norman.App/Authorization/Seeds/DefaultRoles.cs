using Microsoft.AspNetCore.Identity;
using Norman.App.Authorization.Enum;

namespace NourApp.Authorization.Seeds
{
    public static class DefaultRoles
    {
        public static async Task SeedAsync(RoleManager<IdentityRole> roleManager)
        {
            var roles = new List<string>();
            roles.Add(Roles.Admin.ToString());
            roles.Add(Roles.User.ToString());
            if (!roleManager.Roles.Any(e => roles.Contains(e.Name)))
            {
                foreach (var role in roles)
                {
                await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
    }
}
