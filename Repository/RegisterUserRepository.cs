namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using backend.Data;
using Backend.DTO;
using Backend.Models;
public class RegisterUserRepository
{
    private readonly EntityDbContext _context;
    private readonly UserManager<Users> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public RegisterUserRepository(EntityDbContext context, UserManager<Users> userManager, RoleManager<IdentityRole> roleManager)
    {
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    //register user method
    public async Task<string> RegisterUser(RegisterDTO u)
    {
        var user = new Users
        {
            UserName = u.Username,
            Email = u.Email
        };
        
        var result = await _userManager.CreateAsync(user, u.Password);

        if (!result.Succeeded)
            return string.Join(", ", result.Errors.Select(e => e.Description));

        string roleName = "Customer";
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            await _roleManager.CreateAsync(new IdentityRole(roleName));
        }
        
        await _userManager.AddToRoleAsync(user, roleName);
        
        return $"User {user.UserName} created successfully!";
    }
}