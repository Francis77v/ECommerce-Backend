using System.ComponentModel.DataAnnotations;

namespace Backend.DTO
{
    public class ProductAddDTO
    {
        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot exceed 100 characters.")]
        public string ProductName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string ProductDescription { get; set; } = string.Empty;

        [Required(ErrorMessage = "Product price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between 0.01 and 999,999.99.")]
        public float ProductPrice { get; set; }

        // Optional DiscountPrice if you plan to add it later:
        // [Range(0, 999999.99, ErrorMessage = "Discount price must be a positive number.")]
        // public float? DiscountPrice { get; set; }

        [Required(ErrorMessage = "Stock is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Stock cannot be negative.")]
        public int Stock { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive integer.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Brand ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Brand ID must be a positive integer.")]
        public int BrandId { get; set; }
    }
}