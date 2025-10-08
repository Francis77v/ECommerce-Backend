namespace backend.Data;
using Microsoft.EntityFrameworkCore;
using Backend.Models;

public class EntityDbContext : DbContext
{
    public EntityDbContext(DbContextOptions<EntityDbContext> options) : base(options){}

    public DbSet<Product> Product { get; set; }
    public DbSet<Brand> Brand { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .IsRequired();
        
        modelBuilder.Entity<Product>()
            .HasOne(b => b.Brand)
            .WithMany(p => p.Products)
            .HasForeignKey(b => b.BrandId)
            .IsRequired();
        
        modelBuilder.Entity<ProductImage>()
            .HasOne(p => p.Product)
            .WithMany(i => i.ProductImages)
            .HasForeignKey(p => p.ProductId)
            .IsRequired();


        var seedDate = new DateTime(2025, 10, 8, 0, 0, 0, DateTimeKind.Utc);

        modelBuilder.Entity<Brand>().HasData(
            new Brand
            {
                BrandId = -1,
                BrandName = "Samsung"
            }
        );

        modelBuilder.Entity<Category>().HasData(
            new Category
            {
                CategoryId = -1,
                CategoryName = "Phone"
            }
        );

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ProductId = -1, // always include PK when seeding
                ProductName = "Sample Product 1",
                Description = "This is a sample description.",
                Price = 199.99f,
                DiscountPrice = 149.99f,
                Stock = 50,
                CategoryId = -1, // foreign key
                BrandId = -1,    // foreign key
                IsActive = true,
                CreatedAt = seedDate,
                UpdatedAt = seedDate
            }
        );

    }
}