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

    public async Task<List<ProductDTO>> GetAllProductsAsync()
    {
        return await _context.Product
            .Include(p => p.Brand)
            .Include(p => p.Category)
            .Select(p => new ProductDTO
            {
                ProductName = p.ProductName,
                ProductDescription = p.Description,
                ProductPrice = p.Price,
                BrandId = p.Brand.BrandId,
                CategoryId = p.Category.CategoryId,
                Stock = p.Stock,
                CategoryName = p.Category.CategoryName,
                BrandName = p.Brand.BrandName
            }).ToListAsync();
    }

    public async Task<string> AddProductsAsync(ProductDTO p)
    {
        try
        {
            // 🧩 Map ProductDTO to Product entity
            var product = new Product
            {
                ProductName = p.ProductName,
                Description = p.ProductDescription,
                Price = p.ProductPrice,
                Stock = p.Stock,
                CategoryId = p.CategoryId,
                BrandId = p.BrandId
            };

            // 💾 Add to DbContext
            _context.Product.Add(product);

            // 🚀 Save changes
            await _context.SaveChangesAsync();

            // ✅ Return success message
            return $"✅ Product '{product.ProductName}' added successfully with ID {product.ProductId}.";
        }
        catch (Exception ex)
        {
            // ⚠️ Handle errors and return readable message
            return $"❌ Failed to add product: {ex.Message}";
        }
    }

    public async Task<string> DeleteProductAsync(int productId)
    {
        var product = await _context.Product.SingleOrDefaultAsync(p => p.ProductId == productId);
        if (product is null)
        {
            return "Product doesn't exist.";
        }
        _context.Product.Remove(product);
        await _context.SaveChangesAsync();
        return "Product deleted";
    }

    public async Task<string> UpdateProductAsync(int productId, ProductDTO productDto)
    {
        var product = await _context.Product.SingleOrDefaultAsync((p => p.ProductId == productId));
        if (product is null)
        {
            return "Product doesn't exist";
        }
        product.ProductName = productDto.ProductName;
        product.Description = productDto.ProductDescription;
        product.Price = productDto.ProductPrice;
        product.Stock = productDto.Stock;
        product.CategoryId = productDto.CategoryId;
        productDto.BrandId = productDto.BrandId;
        await _context.SaveChangesAsync();
        return $"Product {productId} updated succesfully";

    }

}