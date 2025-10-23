using System.Security.Claims;
using Backend.DTO;
using Backend.Models;
using Backend.Repository;

namespace Backend.Services;

public class UserServices
{
    private readonly UserRepository _repository;
    private readonly ILogger<UserServices> _logger;

    public UserServices(UserRepository repository, ILogger<UserServices> logger)
    {
        _repository = repository;
        _logger = logger;
    }
    
    public async Task<List<UserGetDTO>> GetAllUsersAsync()
    {
        return await _repository.GetUserAsync();
    }
    
    public async Task<(bool Success, string Message, UserGetDTO? Data)> GetUserByIdAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return (false, "Invalid user ID.", null);

        var user = await _repository.GetUserByIdAsync(id);
        if (user == null)
            return (false, "User not found.", null);

        return (true, "User retrieved successfully.", user);
    }
    
    public async Task<(bool Success, string Message)> CreateUserAsync(UserAddDTO dto)
    {
        if (dto == null)
            return (false, "Invalid request data.");

        if (string.IsNullOrWhiteSpace(dto.UserName) || string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
            return (false, "Username, email, and password are required.");

        if (!dto.Email.Contains("@"))
            return (false, "Invalid email format.");

        try
        {
            return await _repository.CreateUserAsync(dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CreateUserAsync Service");
            return (false, "Unexpected error occurred while creating user.");
        }
    }


    public async Task<(bool Success, string Message)> UpdateUserAsync(string id, UserUpdateDTO dto)
    {
        if (string.IsNullOrWhiteSpace(id))
            return (false, "Invalid user ID.");

        if (dto == null)
            return (false, "Invalid update data.");

        try
        {
            return await _repository.UpdateUserAsync(id, dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateUserAsync Service");
            return (false, "Unexpected error occurred while updating user.");
        }
    }
    
    public async Task<(bool Success, string Message)> UpdatePasswordAsync(string id, UserUpdatePasswordDTO dto)
    {
        if (string.IsNullOrWhiteSpace(id))
            return (false, "Invalid user ID.");

        if (dto == null)
            return (false, "Invalid password data.");

        if (string.IsNullOrWhiteSpace(dto.currentPassword) || string.IsNullOrWhiteSpace(dto.newPassword))
            return (false, "Both current and new passwords are required.");

        if (dto.currentPassword == dto.newPassword)
            return (false, "New password must be different from the current password.");

        try
        {
            return await _repository.UpdatePasswordAsync(id, dto);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdatePasswordAsync Service");
            return (false, "Unexpected error occurred while updating password.");
        }
    }
    
    public async Task<(bool Success, string Message)> DeleteUserAsync(string id)
    {
        if (string.IsNullOrWhiteSpace(id))
            return (false, "Invalid user ID.");

        try
        {
            return await _repository.DeleteUserAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in DeleteUserAsync Service");
            return (false, "Unexpected error occurred while deleting user.");
        }
    }
    
    public async Task<(bool Success, string Message, Users? Data)> GetCurrentUserAsync(ClaimsPrincipal principal)
    {
        if (principal == null)
            return (false, "Invalid user context.", null);

        try
        {
            var user = await _repository.GetCurrentUserAsync(principal);
            if (user == null)
                return (false, "User not found or not logged in.", null);

            return (true, "User retrieved successfully.", user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching current user");
            return (false, "Unexpected error occurred while fetching current user.", null);
        }
    }
}

