using Microsoft.AspNetCore.Identity;

namespace Backend.Models.Seeders;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using backend.Data;
using System;
using System.Linq;



public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        var hasher = new PasswordHasher<Users>();
        using (var context = new ApplicationDbContext(
                   serviceProvider.GetRequiredService<
                       DbContextOptions<ApplicationDbContext>>()))
        {
            // Look for any movies.
            if (context.Users.Any())
            {
                return;   // DB has been seeded
            }
            context.Users.AddRange(
                new Users
                {
                    Id = "1",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "Admin123!")
                }
               
            );
            context.SaveChanges();
        }
    }
}