using Backend.DTO;
using Backend.Services;

namespace Backend.APIEndpoints
{
    public static class UserEndpoints
    {
        public static void MapUserEndpoints(this WebApplication app)
        {
            app.MapPost("/api/login", async (LoginRequest login, AuthService authService) =>
            {
                var token = await authService.ValidateUserAsync(login.Username, login.Password);
                if (token != null)
                {
                    return Results.Ok(new
                    {
                        message = "Login successful",
                        token = token
                    });
                }
                return Results.BadRequest("Invalid login");
            });
            app.MapPost("/api/register", async (UserCreateDTO user, UserServices services) =>
            {
                var results = await services.AddUserService(user);
                return Results.Ok(results);
            });

            // app.MapPost("/users", async (UserManager<Users> userManager, Users user) =>
            // {
            //     var result = await userManager.CreateAsync(user, "DefaultPassword123!");
            //     if (!result.Succeeded) return Results.BadRequest(result.Errors);
            //     return Results.Ok(user);
            // });
        }
    }
}