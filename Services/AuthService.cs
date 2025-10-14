using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Backend.Models;
using Microsoft.IdentityModel.Tokens;

namespace Backend.Services;

public class AuthService
{
    private readonly UserManager<Users> _userManager;
    private readonly IConfiguration _config;
    
    public AuthService(UserManager<Users> userManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _config = configuration;
    }
    
    public async Task<String?> ValidateUserAsync(string username, string password)
    {
        var users = await _userManager.FindByNameAsync(username);
        if (users != null && await _userManager.CheckPasswordAsync(users, password))
        {
            return await GenerateJwtToken(users); 
        }
        return null;
    }

    private async Task<string> GenerateJwtToken(Users user)
    {
        var userRoles = await _userManager.GetRolesAsync(user);

        var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            }
            .Concat(userRoles.Select(role => new Claim(ClaimTypes.Role, role)))
            .ToList();

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(_config["Jwt:ExpireMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

}