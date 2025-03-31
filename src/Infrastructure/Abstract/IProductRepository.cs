using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Abstract
{
    public interface IProductRepository
    {
        Task<Product> AddAsync(Product product);
        Task<Product> GetByIdAsync(int id);
        Task<List<Product>> GetAllAsync();
        Task<Product> UpdateAsync(Product product);
        Task<List<Product>> GetProductsByCategoryId(int categoryId);

    }
}