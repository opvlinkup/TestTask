using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public async Task<Order> GetOrder()
        {
            return await _context.Orders.Where(order => order.Quantity > 1)
                .OrderByDescending(order => order.CreatedAt).
                FirstOrDefaultAsync();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await _context.Orders.Where(order=>order.User.Status == UserStatus.Active)
                .OrderBy(order => order.CreatedAt)
                .ToListAsync();
        }
    }
}
