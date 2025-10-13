namespace Backend.DTO;

public class ProductGetDTO
{
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public float ProductPrice { get; set; }
    // public float DiscountPrice { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public int BrandId { get; set; }
    public string? CategoryName { get; set; }
    public string? BrandName { get; set; }
}