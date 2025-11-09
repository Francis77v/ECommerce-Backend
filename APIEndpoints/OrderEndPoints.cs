using Backend.DTO;
using Backend.Services;
namespace Backend.APIEndpoints;

public static class OrderEndPoints
{
    
    public static void MapOrderEndpoints(this WebApplication app)
    {
        // public endpoints (e.g. register)
        var Group = app.MapGroup("api/order").WithTags("Order").WithOpenApi()
            .RequireAuthorization(policy => policy.RequireRole("Admin", "Manager"));
        
        Group.MapPost("/", async (PlaceOrderDTO dto, OrderServices service) =>
        {
            try
            {
                var order = await service.PlaceOrderService(dto);
                return Results.Ok(order);
            }
            catch (Exception e)
            {
                return Results.BadRequest($"Error fetching database : {e.Message}");
            }
            
        }).WithName("GetProducts");

    //     Group.MapPost("/add", async (ProductAddDTO productAddDto, ProductServices services) =>
    //     {
    //         var addProducts = await services.AddProductService(productAddDto);
    //         return Results.Ok(addProducts);
    //     }).WithName("AddProducts");
    //     
    //     Group.MapDelete("/{productId}", async (int productId, ProductServices services) =>
    //     {
    //         var deleteProduct = await services.DeleteProductService(productId);
    //         return Results.Ok(deleteProduct);
    //     }).WithName("DeleteProducts");
    //
    //     Group.MapPut("update/{productId}",
    //         async (int productId, ProductGetDTO productGetDto, ProductServices services) =>
    //         {
    //             var updateProduct = await services.UpdateProductService(productId, productGetDto);
    //             return updateProduct;
    //         }).WithName("UpdateProducts");
    }                                   
}