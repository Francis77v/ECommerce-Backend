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

    public async Task<List<ProductGetDTO>> GetProductService()
    {
        return await _productRepository.GetProductsAsync();
    }

    public async Task<string> AddProductService(ProductAddDTO productAddDto)
    {
        if (await _productRepository.CheckProductExist(productAddDto.ProductName))
        {
            return $"{productAddDto.ProductName} already exists.";
        }
        return await _productRepository.AddProductsAsync(productAddDto);
    }

    public async Task<string> DeleteProductService(int productId)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            return "Product doesn't exist.";
        }
        return await _productRepository.DeleteProductAsync(product);
    }

    public async Task<string> UpdateProductService(int productId, ProductGetDTO productGetDto)
    {
        var product = await _productRepository.GetProductByIdAsync(productId);
        if (product is null)
        {
            return "Product doesn't exist.";
        }
        return await _productRepository.UpdateProductAsync(productId, product, productGetDto);
    }
}