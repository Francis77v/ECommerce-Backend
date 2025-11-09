namespace Backend.Services;
using backend.Data;
using Backend.DTO;
public class OrderServices
{
    private readonly EntityDbContext _context;

    public OrderServices(EntityDbContext context)
    {
        _context = context;
    }

    public async Task<string> PlaceOrderService(PlaceOrderDTO orderDto)
    {
        
    }
}