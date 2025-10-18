using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using backend.Data;
using System;
using System.Linq;

namespace Backend.Models.Seeders
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new ApplicationDbContext(
                       serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>()))
            {
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                string[] roles = new[] { "Admin", "Customer" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                }

                // 2️⃣ Seed new Admin user only
                var userManager = serviceProvider.GetRequiredService<UserManager<Users>>();
                
                // Seed new admin
                // if (!context.Users.Any(u => u.UserName == "admin123"))
                // {
                //     var adminUser = new Users
                //     {
                //         UserName = "admin123",
                //         NormalizedUserName = "ADMIN123",
                //         Email = "admin123@example.com",
                //         NormalizedEmail = "ADMIN123@EXAMPLE.COM",
                //         EmailConfirmed = true
                //     };
                //
                //     var result = userManager.CreateAsync(adminUser, "Admin123!").Result;
                //
                //     if (result.Succeeded)
                //     {
                //         userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                //     }
                // }
                
                if (!context.Users.Any(u => u.UserName == "zayn123"))
                {
                    var normalUser = new Users
                    {
                        UserName = "zayn123",
                        NormalizedUserName = "ZAYN123",
                        Email = "zayn123@example.com",
                        NormalizedEmail = "ZAYN123@EMAIL.COM",
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(normalUser, "Zayn123!").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(normalUser, "Customer").Wait();
                    }
                }
            }
        }
    }
}
