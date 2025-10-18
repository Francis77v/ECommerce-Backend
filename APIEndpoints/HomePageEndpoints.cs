using Backend.DTO;
using Backend.Repository;
using Backend.Services;

namespace Backend.APIEndpoints
{
    public static class HomePageEndpoints
    {
        public static void MapHomePageEndpoints(this WebApplication app)
        {
            app.MapPost("/api/login", async (LoginRequest login, AuthRepository authRepository) =>
            {
                var token = await authRepository.ValidateUserAsync(login.Username, login.Password);
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
            app.MapPost("/api/register", async (RegisterDTO user, RegisterUserServices services) =>
            {
                var results = await services.RegisterUserService(user);
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