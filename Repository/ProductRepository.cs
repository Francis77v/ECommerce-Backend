namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Backend.DTO;
using Backend.Models;
public class ProductRepository
{
    private readonly EntityDbContext _context;
    public ProductRepository(EntityDbContext context)
    {
        _context = context;
    }

    public async Task<List<ProductGetDTO>> GetProductsAsync()
    {
      
        return await _context.Product
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Select(p => new ProductGetDTO
            {
                ProductName = p.ProductName,
                ProductDescription = p.Description,
                ProductPrice = p.Price,
                Stock = p.Stock,
                category = new CategoryDTO
                {
                    categoryId = p.Category.CategoryId,
                    categoryName = p.Category.CategoryName
                },
                brand = new BrandDTO
                {
                    brandId = p.Brand.BrandId,
                    brandName = p.Brand.BrandName
                }
            })
            .ToListAsync();
    }

    public async Task<Product?> GetProductByIdAsync(int productId)
    {
        return await _context.Product.SingleOrDefaultAsync(p => p.ProductId == productId);
    }

    public async Task<string> AddProductsAsync(ProductAddDTO p)
    {
        try
        {
            var product = new Product
            {
                ProductName = p.ProductName,
                Description = p.ProductDescription,
                Price = p.ProductPrice,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId
            };
            _context.Product.Add(product);
            await _context.SaveChangesAsync();
            return $"✅ Product '{product.ProductName}' added successfully with ID {product.ProductId}.";
        }
        catch (Exception ex)
        {
            return $"❌ Failed to add product: {ex.Message}";
        }
    }

    public async Task<string> DeleteProductAsync(int productId)
    {
        var product = await GetProductByIdAsync(productId);
        if (product is null)
        {
            return "Product doesn't exist.";
        }
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return "Product deleted";
    }

    public async Task<string> UpdateProductAsync(int productId, ProductGetDTO productGetDto)
    {
        var product = await GetProductByIdAsync(productId);
        if (product is null)
        {
            return "Product doesn't exist.";
        }
        product.ProductName = productGetDto.ProductName;
        product.Description = productGetDto.ProductDescription;
        product.Price = productGetDto.ProductPrice;
        product.Stock = productGetDto.Stock;
        product.CategoryId = productGetDto.category.categoryId;
        productGetDto.brand.brandId = productGetDto.brand.brandId;
        await _context.SaveChangesAsync();
        return $"Product ID: {productId} updated succesfully";

    }

    public async Task<bool> CheckProductExist(string productName)
    {
        return await _context.Product.AnyAsync(p => p.ProductName == productName);
    }

}