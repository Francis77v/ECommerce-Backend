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
    }
}