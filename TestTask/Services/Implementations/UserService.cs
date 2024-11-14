using Microsoft.EntityFrameworkCore;
using TestTask.Data;
using TestTask.Enums;
using TestTask.Models;
using TestTask.Services.Interfaces;

namespace TestTask.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<User> GetUser()
        {
            var userWithMaxTotalGoods2003 = await _context.Users
                .Include(user => user.Orders.Where(order => order.CreatedAt.Year == 2003)).
                Select(user =>
                new
                {
                    User = user,
                    TotalOrders = user.Orders.Where(order => order.CreatedAt.Year == 2003)
                        .Sum(order => order.Quantity)
                })
                .OrderByDescending(seletction => seletction.TotalOrders)
                .FirstOrDefaultAsync();
            return userWithMaxTotalGoods2003?.User;
        }

        public async Task<List<User>> GetUsers()
        {
            var usersWithPaidOrdersIn2010 = await _context.Users.Where(user =>
                    user.Orders.Any(order => order.Status == OrderStatus.Paid && order.CreatedAt.Year == 2010))
                .ToListAsync();
            return usersWithPaidOrdersIn2010;
        }
    }
}
