using System.Security.Claims;
namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
public class UserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Users> _manager;
    public UserRepository(ApplicationDbContext context, UserManager<Users> manager)
    {
        _context = context;
        _manager = manager;
    }

    public async Task<Users?> GetCurrentUserAsync(ClaimsPrincipal user)
    {
        return await _manager.GetUserAsync(user);
    }

    public async Task<List<UserGetDTO>> GetUserAsync()
    {
        try
        {
            return await _manager.Users.Select(u => new UserGetDTO
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();
        }
        catch (Exception ex)
        {
            return new List<UserGetDTO>();
        }
    }


    
    
}