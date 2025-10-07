using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Product
{
    public int ProductId { get; set; }
    
    [Required]
    public string ProductName { get; set; }
    [Column(TypeName = "text")]
    public string? Description { get; set; }
    [Required]
    public float Price { get; set; }
    public float? DiscountPrice { get; set; }
    [Required]
    public int Stock { get; set; }
    
    //foreign keys
    public int CategoryId { get; set; }
    [ForeignKey("CategoryId")]
    public Category Category { get; set; }
    
    public int BrandId { get; set; }
    [ForeignKey("BrandId")]
    public Brand Brand { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    
    public bool IsActive { get; set; }
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
    
    
}