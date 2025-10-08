namespace Backend.Services;
using Backend.Models;
using Backend.Repository;
public class ProductServices
{
    private readonly ProductRepository _productRepository; 
    public ProductServices(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<Product>> GetProductAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }
}