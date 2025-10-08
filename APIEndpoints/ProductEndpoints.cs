using Backend.DTO;
using Backend.Services;
using Backend.Repository;
namespace Backend.APIEndpoints
{
    public static class ProductEndpoint
    {
        public static void MapProductEndpoints(this WebApplication app)
        {
            app.MapGet("api/products", async (ProductServices services) =>
            {
                var products = await services.GetProductAsync();
                return Results.Ok(products);
            });
        }
    }
}