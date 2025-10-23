namespace Backend.APIEndpoints;
using Backend.DTO;
using Backend.Services;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        // public endpoints (e.g. register)
        var anonGroup = app.MapGroup("api/users").WithTags("Users");

        // Create user (registration) — allow anonymous
        anonGroup.MapPost("/", async (UserAddDTO dto, UserServices service) =>
        {
            var (success, message) = await service.CreateUserAsync(dto);
            if (!success) return Results.BadRequest(new { success = false, message });
            // Created — no resource id returned from service/repo, return 201 with message
            return Results.Created($"/api/users", new { success = true, message });
        }).AllowAnonymous();

        // protected endpoints — require authenticated user
        var group = app.MapGroup("api/users")
            .WithTags("Users");
                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                            

        // Get all users
        group.MapGet("/", async (UserServices service) =>
        {
            var users = await service.GetAllUsersAsync();
            return Results.Ok(users);
        });

        // Get current logged-in user
        group.MapGet("/me", async (ClaimsPrincipal user, UserServices service) =>
        {
            var (success, message, data) = await service.GetCurrentUserAsync(user);
            if (!success) return Results.Unauthorized();
            return Results.Ok(new { success, message, user = data });
        });

        // Get user by id
        group.MapGet("/{id}", async (string id, UserServices service) =>
        {
            var (success, message, data) = await service.GetUserByIdAsync(id);
            if (!success && data == null)
            {
                // if invalid id or not found
                return Results.NotFound(new { success = false, message });
            }
            return Results.Ok(new { success = true, message, data });
        });

        // Update user (info)
        group.MapPut("/{id}", async (string id, UserUpdateDTO dto, UserServices service) =>
        {
            var (success, message) = await service.UpdateUserAsync(id, dto);
            if (!success) return Results.BadRequest(new { success = false, message });
            return Results.Ok(new { success = true, message });
        });

        // Update password
        group.MapPut("/{id}/password", async (string id, UserUpdatePasswordDTO dto, UserServices service) =>
        {
            var (success, message) = await service.UpdatePasswordAsync(id, dto);
            if (!success) return Results.BadRequest(new { success = false, message });
            return Results.Ok(new { success = true, message });
        });

        // Delete user
        group.MapDelete("/{id}", async (string id, UserServices service) =>
        {
            var (success, message) = await service.DeleteUserAsync(id);
            if (!success) return Results.BadRequest(new { success = false, message });
            return Results.Ok(new { success = true, message });
        });
    }
}
