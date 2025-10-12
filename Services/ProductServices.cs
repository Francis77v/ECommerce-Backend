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

    public async Task<List<ProductDTO>> GetProductService()
    {
        return await _productRepository.GetProductsAsync();
    }

    public async Task<string> AddProductService(ProductDTO productDto)
    {
        return await _productRepository.AddProductsAsync(productDto);
    }

    public async Task<string> DeleteProductService(int productId)
    {
        return await _productRepository.DeleteProductAsync(productId);
    }

    public async Task<string> UpdateProductService(int productId, ProductDTO productDto)
    {
        return await _productRepository.UpdateProductAsync(productId, productDto);
    }
}