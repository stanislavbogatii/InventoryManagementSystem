using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Infrastructure.Services
{
    public class ProductPropertiesFactory
    {
        private readonly Dictionary<string, ProductProperties> _cache = new();
        private readonly IProductPropertiesRepository _repository;

        public ProductPropertiesFactory(IProductPropertiesRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProductProperties> GetItemPropertiesAsync(string? modelName, double? weight, string? dimensions, string? color, string? category)
        {
            string key = $"{modelName}_{weight}_{dimensions}_{color}_{category}";
            if (!_cache.ContainsKey(key))
            {
                var properties = await _repository.GetByAttributesAsync(modelName, weight, dimensions, color, category);
                if (properties == null)
                {
                    properties = new ProductProperties(modelName, weight, dimensions, color, category);
                    await _repository.AddAsync(properties);
                }
                _cache[key] = properties;
            }
            return _cache[key];
        }

    }
}
