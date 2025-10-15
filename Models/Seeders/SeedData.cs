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
                // 1️⃣ Seed Roles
                var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                string[] roles = new[] { "Admin", "User" };

                foreach (var role in roles)
                {
                    if (!roleManager.RoleExistsAsync(role).Result)
                    {
                        roleManager.CreateAsync(new IdentityRole(role)).Wait();
                    }
                }

                // 2️⃣ Seed new Admin user only
                var userManager = serviceProvider.GetRequiredService<UserManager<Users>>();

                // Remove any previous admin users with the old username (optional)
                var oldAdmins = context.Users.Where(u => u.UserName == "admin" || u.UserName == "admin123").ToList();
                if (oldAdmins.Any())
                {
                    foreach (var oldAdmin in oldAdmins)
                    {
                        userManager.DeleteAsync(oldAdmin).Wait();
                    }
                    context.SaveChanges();
                }

                // Seed new admin
                if (!context.Users.Any(u => u.UserName == "admin123"))
                {
                    var adminUser = new Users
                    {
                        UserName = "admin123",
                        NormalizedUserName = "ADMIN123",
                        Email = "admin123@example.com",
                        NormalizedEmail = "ADMIN123@EXAMPLE.COM",
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(adminUser, "Admin123!").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(adminUser, "Admin").Wait();
                    }
                }

                // Optionally, seed a normal user
                if (!context.Users.Any(u => u.UserName == "user"))
                {
                    var normalUser = new Users
                    {
                        UserName = "user",
                        NormalizedUserName = "USER",
                        Email = "user@example.com",
                        NormalizedEmail = "USER@EXAMPLE.COM",
                        EmailConfirmed = true
                    };

                    var result = userManager.CreateAsync(normalUser, "User123!").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(normalUser, "User").Wait();
                    }
                }
            }
        }
    }
}
