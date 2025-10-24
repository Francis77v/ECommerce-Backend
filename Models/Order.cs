using System.ComponentModel.DataAnnotations.Schema;

namespace Backend.Models;

public class Order
{
    public int OrderId { get; set; }

    // Relation: one order can have multiple items
    public ICollection<OrderItem> Items { get; set; } = new List<OrderItem>();

    // Financials
    public decimal Subtotal { get; set; }
    public decimal ShippingFee { get; set; }
    public decimal Tax { get; set; }
    public decimal TotalAmount { get; set; }

    // Order Status
    public string Status { get; set; } = "Pending"; // e.g., Pending, Paid, Shipped, Delivered, Cancelled

    // Payment Information
    public string PaymentMethod { get; set; } // e.g., "Credit Card", "GCash", "COD"
    public string PaymentStatus { get; set; } = "Unpaid"; // e.g., "Unpaid", "Paid", "Refunded"

    // Shipping / Delivery Info
    public string ShippingAddress { get; set; }
    public string BillingAddress { get; set; }
    public string TrackingNumber { get; set; }
    public DateTime? ShippedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }

    // Audit
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }

    // Relationship to User
    public string UserId { get; set; }
    [ForeignKey("UserId")]
    public Users User { get; set; }
}