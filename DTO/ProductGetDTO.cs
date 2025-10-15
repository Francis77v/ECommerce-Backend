using System.ComponentModel.DataAnnotations;

namespace Backend.DTO;

public class ProductGetDTO
{
    [Required(ErrorMessage = "Product name is required.")]
    [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
    public string ProductName { get; set; }

    [Required(ErrorMessage = "Product description is required.")]
    [StringLength(500, ErrorMessage = "Product description cannot exceed 500 characters.")]
    public string ProductDescription { get; set; }

    [Required(ErrorMessage = "Product price is required.")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Product price must be greater than zero.")]
    public float ProductPrice { get; set; }

    [Required(ErrorMessage = "Stock quantity is required.")]
    [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
    public int Stock { get; set; }

    [Required(ErrorMessage = "Category ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number.")]
    public int CategoryId { get; set; }

    [Required(ErrorMessage = "Brand ID is required.")]
    [Range(1, int.MaxValue, ErrorMessage = "Brand ID must be a positive number.")]
    public int BrandId { get; set; }

    // Optional fields (no validation needed unless you want to restrict length)
    [StringLength(100, ErrorMessage = "Category name cannot exceed 100 characters.")]
    public string? CategoryName { get; set; }

    [StringLength(100, ErrorMessage = "Brand name cannot exceed 100 characters.")]
    public string? BrandName { get; set; }
}