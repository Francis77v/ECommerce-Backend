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
            app.MapPost("api/product/add", async (ProductDTO productDto, ProductServices services) =>
            {
                var addProducts = await services.AddProductAsync(productDto);
                return Results.Ok(addProducts);
            });
            app.MapDelete("api/product/{productId}", async (int productId, ProductServices services) =>
            {
                var deleteProduct = await services.DeleteProductService(productId);
                return Results.Ok(deleteProduct);
            });
        }                                   
    }
}