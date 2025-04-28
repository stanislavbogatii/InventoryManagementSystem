using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class ProductPropertiesRepository : IProductPropertiesRepository
    {
        private readonly InventoryDbContext _context;

        public ProductPropertiesRepository(InventoryDbContext context)
        {
            _context = context;
        }

        public async Task<ProductProperties> GetByAttributesAsync(string? modelName, double? weight, string? dimensions, string? color, string? category)
        {
            var entity = await _context.ProductProperties
                .FirstOrDefaultAsync(p => p.ModelName == modelName &&
                                         p.Weight == weight &&
                                         p.Dimensions == dimensions &&
                                         p.Color == color &&
                                         p.Category == category);
            return entity;
        }

        public async Task AddAsync(ProductProperties properties)
        {
            _context.ProductProperties.Add(properties);
            await _context.SaveChangesAsync();
        }
    }
}
