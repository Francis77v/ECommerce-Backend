namespace Backend.Repository;
using Microsoft.EntityFrameworkCore;
using backend.Data;
using Backend.Models;
public class ProductRepository
{
    private readonly EntityDbContext _context;
    public ProductRepository(EntityDbContext context)
    {
        _context = context;
    }

    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await _context.Product
            .Include(p => p.Brand)
            .Include(p => p.Category)
            // .Include(p => p.ProductImages)
            .ToListAsync();
    }
    
}