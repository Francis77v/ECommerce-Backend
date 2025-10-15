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
                var products = await services.GetProductService();
                return Results.Ok(products);
            }).WithName("GetProducts").WithOpenApi()
                .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));
            
            app.MapPost("api/products/add", async (ProductAddDTO productAddDto, ProductServices services) =>
            {
                var addProducts = await services.AddProductService(productAddDto);
                return Results.Ok(addProducts);
            }).WithName("AddProducts").WithOpenApi().RequireAuthorization(policy => policy.RequireRole("Admin"));
            app.MapDelete("api/products/{productId}", async (int productId, ProductServices services) =>
            {
                var deleteProduct = await services.DeleteProductService(productId);
                return Results.Ok(deleteProduct);
            }).WithName("DeleteProducts").WithOpenApi().RequireAuthorization(policy => policy.RequireRole("Admin"));
            app.MapPut("api/products/update/{productId}", async (int productId, ProductGetDTO productGetDto, ProductServices services) =>
            {
                var updateProduct = await services.UpdateProductService(productId, productGetDto);
                return updateProduct;
            }).WithName("UpdateProducts").WithOpenApi().RequireAuthorization(policy => policy.RequireRole("Admin"));
        }                                   
    }
}