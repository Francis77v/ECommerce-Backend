using Backend.DTO;
using Backend.Repository;
using Backend.Services;

namespace Backend.APIEndpoints
{
    public static class HomePageEndpoints
    {
        public static void MapHomePageEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("/api").WithTags("HomePage").WithOpenApi().AllowAnonymous();
            group.MapPost("/login", async (LoginRequest login, AuthRepository authRepository) =>
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
            group.MapPost("/register", async (RegisterDTO user, RegisterUserServices services) =>
            {
                var results = await services.RegisterUserService(user);
                return Results.Ok(results);
            });

        }
    }
}