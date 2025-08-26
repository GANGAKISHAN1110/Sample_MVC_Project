using SampleMvcproject.Application.DTOs;
using SampleMvcproject.Data;
using SampleMvcproject.Domain.Entities;
using SampleMvcproject.Domain.Interfaces;

namespace SampleMvcProject.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;

        public ProductRepository(ProductDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await Task.FromResult(_context.Products.Select(p => new Product
            {
                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price
            }).ToList());
        }

        public async Task<Product?> GetByIdAsync(int id)
        {
            var exist = await _context.Products.FindAsync(id);
            if(exist == null)
            {
                return null;
            }
            return new Product
            {
                Id = exist.Id,
                Name = exist.Name,
                Description = exist.Description,
                Price = exist.Price
            };
        }

        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var p = await _context.Products.FindAsync(id);
            if(p != null)
            {
                _context.Products.Remove(p);
                await _context.SaveChangesAsync();
            }
        }
    }
}
