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

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<User> GetUser()
        {
            return await _context.Users
                .OrderByDescending(u => u.Orders.Sum(o => o.Quantity)).Select(u => new
                User
                {
                    Id = u.Id,
                    Email = u.Email,
                    Status = u.Status,
                    Orders = _context.Orders.Where(o => o.UserId == u.Id).ToList()
                })
                .FirstOrDefaultAsync();
        }

         private async Task<List<User>> GetInactiveUsers()
          {
              return await _context.Users.Where(u => u.Status == UserStatus.Inactive).Select(u => new
                  User
                  {
                      Id = u.Id,
                      Email = u.Email,
                      Status = u.Status,
                      Orders = _context.Orders.Where(o => o.UserId == u.Id).ToList()
                  }).ToListAsync();
          } 

        public async Task<List<User>> GetUsers()
        {
            return await GetInactiveUsers();
        }
    }

}
