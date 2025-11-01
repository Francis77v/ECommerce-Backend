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
                
        // dtos
        public BrandDTO brand { get; set; }
        public CategoryDTO category { get; set; }
    }