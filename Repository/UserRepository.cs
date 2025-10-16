namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using backend.Data;
using Backend.DTO;
using Backend.Models;
public class UserRepository
{
    private readonly EntityDbContext _context;
    private readonly UserManager<Users> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(EntityDbContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<string> AddUser(UserCreateDTO u)
    {
        var user = new Users
        {
            UserName = u.Username,
            Email = u.Email
        };

 
        var result = await _userManager.CreateAsync(user, u.Password);

        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));


        if (!string.IsNullOrEmpty(u.Role))
        {
            if (!await _roleManager.RoleExistsAsync(u.Role))
            {
                await _roleManager.CreateAsync(new IdentityRole(u.Role));
            }

            await _userManager.AddToRoleAsync(user, u.Role);
        }
        return $"User {user.UserName} created successfully with role {u.Role}!";
    }

}