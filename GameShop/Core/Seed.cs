using GameShopModel.Entities;
using Microsoft.AspNetCore.Identity;

namespace GameShop.Core
{
    public class Seed
    {
        public static async Task SeedUserAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using var applicationServices = applicationBuilder.ApplicationServices.CreateScope();
            var roleManager = applicationServices.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.User))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.User));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Moderator))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Moderator));
            }

            if (!await roleManager.RoleExistsAsync(UserRoles.Manager))
            {
                await roleManager.CreateAsync(new IdentityRole(UserRoles.Manager));
            }

            var userManager = applicationServices.ServiceProvider.GetRequiredService<UserManager<User>>();
            string adminEmail = "admin@yandex.ru";
            var adminUser = await userManager.FindByNameAsync(adminEmail);

            if (adminUser is null)
            {
                var user = new User
                {
                    UserName = "Admin",
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Admin!@159753");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Admin);
                }
            }

            string userEmail = "user@yandex.ru";
            var simpleUser = await userManager.FindByNameAsync(userEmail);

            if (simpleUser is null)
            {
                var user = new User
                {
                    UserName = "User",
                    Email = userEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "User!@159753");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.User);
                }
            }

            string moderatorEmail = "moderator@yandex.ru";
            var workUser = await userManager.FindByNameAsync(moderatorEmail);

            if (workUser is null)
            {
                var user = new User
                {
                    UserName = "Moderator",
                    Email = moderatorEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Moderator!@159753");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Moderator);
                }
            }

            string managerEmail = "manager@yandex.ru";
            var managementUser = await userManager.FindByNameAsync(managerEmail);

            if (workUser is null)
            {
                var user = new User
                {
                    UserName = "manager",
                    Email = managerEmail,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, "Manager!@159753");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, UserRoles.Manager);
                }
            }


        }
    }
}
