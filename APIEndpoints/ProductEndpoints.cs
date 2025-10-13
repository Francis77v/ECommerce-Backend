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
            }).WithName("GetProducts").WithOpenApi();
            app.MapPost("api/product/add", async (ProductAddDTO productAddDto, ProductServices services) =>
            {
                var addProducts = await services.AddProductService(productAddDto);
                return Results.Ok(addProducts);
            }).WithName("AddProducts").WithOpenApi();
            app.MapDelete("api/product/{productId}", async (int productId, ProductServices services) =>
            {
                var deleteProduct = await services.DeleteProductService(productId);
                return Results.Ok(deleteProduct);
            }).WithName("DeleteProducts").WithOpenApi();
            app.MapPut("api/product/update/{productId}", async (int productId, ProductGetDTO productGetDto, ProductServices services) =>
            {
                var updateProduct = await services.UpdateProductService(productId, productGetDto);
                return updateProduct;
            }).WithName("UpdateProducts").WithOpenApi();
        }                                   
    }
}