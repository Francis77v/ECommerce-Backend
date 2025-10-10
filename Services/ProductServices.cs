namespace Backend.Services;
using Backend.Models;
using Backend.Repository;
using Backend.DTO;
public class ProductServices
{
    private readonly ProductRepository _productRepository; 
    public ProductServices(ProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<List<ProductDTO>> GetProductAsync()
    {
        return await _productRepository.GetAllProductsAsync();
    }

    public async Task<string> AddProductAsync(ProductDTO productDto)
    {
        return await _productRepository.AddProductsAsync(productDto);
    }

    public async Task<string> DeleteProductService(int productId)
    {
        return await _productRepository.DeleteProductAsync(productId);
    }
}