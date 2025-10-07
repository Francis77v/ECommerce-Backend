using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class ProductImage
{
    public int ProductImageId { get; set; }
    //foreign key
    public int ProductId { get; set; }
    [ForeignKey(nameof(ProductId))]
    public Product Product { get; set; }
    
    //fields
    
    [Required]
    public byte[] ImageData { get; set; }

    // To store file type (e.g. image/png, image/jpeg)
    [MaxLength(100)]
    public string ContentType { get; set; }
    
  
}