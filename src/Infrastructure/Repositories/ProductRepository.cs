using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;
using Microsoft.EntityFrameworkCore;
using InventoryManagement.Infrastructure.Services;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly InventoryDbContext _context;
        private readonly ProductPropertiesFactory _factory;

        public ProductRepository(InventoryDbContext context, ProductPropertiesFactory factory)
        {
            _context = context;
            _factory = factory;
        }

        public async Task<Product> AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            return await _context.Products
                .Include(p => p.Properties)
                .FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<List<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(p => p.Properties)
                .ToListAsync();
        }

        public async Task<Product> UpdateAsync(Product product)
        {
            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new Exception("Product not found");
            }

            _context.Entry(existingProduct).CurrentValues.SetValues(product);
            existingProduct.LastUpdated = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingProduct;
        }

        public async Task<List<Product>> GetProductsByCategoryId(int categoryId)
        {
            return await _context.Products.Where(p => p.CategoryId == categoryId).ToListAsync();
        }
    }
}
