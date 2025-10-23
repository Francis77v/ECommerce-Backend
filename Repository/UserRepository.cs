using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Backend.DTO;
using Backend.Models;
using Microsoft.AspNetCore.Identity;

namespace Backend.Repository;
public class UserRepository
{
    private readonly ApplicationDbContext _context;
    private readonly UserManager<Users> _manager;
    private readonly SignInManager<Users> _signInManager;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(
        ApplicationDbContext context,
        UserManager<Users> manager,
        SignInManager<Users> signInManager,
        ILogger<UserRepository> logger)
    {
        _context = context;
        _manager = manager;
        _signInManager = signInManager;
        _logger = logger;
    }

    public async Task<Users?> GetCurrentUserAsync(ClaimsPrincipal user)
    {
        return await _manager.GetUserAsync(user);
    }

    public async Task<List<UserGetDTO>> GetUserAsync()
    {
        try
        {
            return await _manager.Users
                .Select(u => new UserGetDTO
                {
                    Id = u.Id,
                    UserName = u.UserName,
                    Email = u.Email
                })
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching user list.");
            return new List<UserGetDTO>();
        }
    }

    public async Task<UserGetDTO?> GetUserByIdAsync(string id)
    {
        var user = await _manager.FindByIdAsync(id);
        if (user == null) return null;

        return new UserGetDTO
        {
            Id = user.Id,
            UserName = user.UserName,
            Email = user.Email
        };
    }

    public async Task<(bool Success, string Message)> CreateUserAsync(UserAddDTO dto)
    {
        try
        {
            if (await _manager.Users.AnyAsync(u => u.UserName == dto.UserName))
                return (false, "Username already exists.");

            if (await _manager.Users.AnyAsync(u => u.Email == dto.Email))
                return (false, "Email already exists.");

            var newUser = new Users
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _manager.CreateAsync(newUser, dto.Password);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Failed to create user {Username}: {Errors}", dto.UserName, errors);
                return (false, $"Failed to create user: {errors}");
            }

            return (true, "User created successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Unexpected error while creating user {Username}", dto.UserName);
            return (false, "Internal server error while creating user.");
        }
    }

    public async Task<(bool Success, string Message)> UpdateUserAsync(string id, UserUpdateDTO dto)
    {
        try
        {
            var user = await _manager.FindByIdAsync(id);
            if (user == null)
                return (false, "User not found.");

            if (!string.IsNullOrWhiteSpace(dto.UserName))
            {
                var usernameTaken = await _manager.Users.AnyAsync(u => u.UserName == dto.UserName && u.Id != id);
                if (usernameTaken)
                    return (false, "Username already taken.");
                user.UserName = dto.UserName;
            }

            if (!string.IsNullOrWhiteSpace(dto.Email))
            {
                var emailTaken = await _manager.Users.AnyAsync(u => u.Email == dto.Email && u.Id != id);
                if (emailTaken)
                    return (false, "Email already in use.");
                user.Email = dto.Email;
            }

            var result = await _manager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Failed to update user {Id}: {Errors}", id, errors);
                return (false, $"Failed to update user: {errors}");
            }

            return (true, "User updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating user {Id}", id);
            return (false, "Internal server error while updating user.");
        }
    }

    public async Task<(bool Success, string Message)> UpdatePasswordAsync(string id, UserUpdatePasswordDTO dto)
    {
        try
        {
            var user = await _manager.FindByIdAsync(id);
            if (user == null)
                return (false, "User not found.");

            var result = await _manager.ChangePasswordAsync(user, dto.currentPassword, dto.newPassword);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Password update failed for user {Id}: {Errors}", id, errors);
                return (false, $"Failed to update password: {errors}");
            }

            return (true, "Password updated successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error updating password for user {Id}", id);
            return (false, "Internal server error while updating password.");
        }
    }

    public async Task<(bool Success, string Message)> DeleteUserAsync(string id)
    {
        try
        {
            var user = await _manager.FindByIdAsync(id);
            if (user == null)
                return (false, "User not found.");

            var result = await _manager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                _logger.LogWarning("Failed to delete user {Id}: {Errors}", id, errors);
                return (false, $"Failed to delete user: {errors}");
            }

            return (true, "User deleted successfully.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error deleting user {Id}", id);
            return (false, "Internal server error while deleting user.");
        }
    }
}

