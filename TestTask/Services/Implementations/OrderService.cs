using TestTask.Data;
using TestTask.Models;
using TestTask.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace TestTask.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;
    
        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        } 
        private async Task<Order> GetMaxSumOrder()
        {
            var query = _context.Orders
                .OrderByDescending(o => o.Price * o.Quantity)
                .Select(o => new Order
                {
                    Id = o.Id,
                    ProductName = o.ProductName,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    UserId = o.UserId,
                    User = new User
                    {
                        Id = o.User.Id,
                        Email = o.User.Email,
                        Status = o.User.Status
                    }
                });

            var maxSumOrder = await query.FirstOrDefaultAsync();
            return maxSumOrder;

        }

        private async Task<List<Order>> GetOrdersQuantityGreaterThanTen()
        {
            var query = _context.Orders
                .Where(o => o.Quantity > 10)
                .Select(o => new Order
                {
                    Id = o.Id,
                    ProductName = o.ProductName,
                    Price = o.Price,
                    Quantity = o.Quantity,
                    UserId = o.UserId,
                    User = new User
                    {
                        Id = o.User.Id,
                        Email = o.User.Email,
                        Status = o.User.Status
                    }
                });

            var orders = await query.ToListAsync();
            return orders;
        }
        public async Task<Order> GetOrder()
        {
            return await GetMaxSumOrder();
        }

        public async Task<List<Order>> GetOrders()
        {
            return await GetOrdersQuantityGreaterThanTen();
        }
    }
    
}
