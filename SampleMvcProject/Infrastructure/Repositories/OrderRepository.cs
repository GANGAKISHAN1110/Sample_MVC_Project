using Microsoft.EntityFrameworkCore;
using SampleMvcproject.Data;
using SampleMvcProject.Domain.Entities;
using SampleMvcProject.Domain.Repositories;

namespace SampleMvcProject.Infrastructure.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly ProductDbContext _context;

        public OrderRepository(ProductDbContext context)
        {
            _context = context;
        }

        // Implement methods for IOrderRepository here

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o =>o.OrderItems)
                .ToListAsync();
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer)
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> AddAsync(Order order)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order; // return the saved order
        }

        public async Task<Order> UpdateAsync(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task DeleteAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null) return;
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
        }
    }
}
