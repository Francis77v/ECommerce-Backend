namespace Backend.APIEndpoints;
using Backend.DTO;
using Backend.Services;
public static class UserEndpoints
{
    public static void MapUserEndpoints(this WebApplication app)
    {
        app.MapGet("api/users/get", async (UserServices service) =>
        {
            return await service.GetUserService();
        });
    }
}