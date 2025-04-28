using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Abstract
{
    public interface IProductPropertiesRepository
    {
        Task<ProductProperties> GetByAttributesAsync(string? modelName, double? weight, string? dimensions, string? color, string? category);
        Task AddAsync(ProductProperties properties);
    }
}
