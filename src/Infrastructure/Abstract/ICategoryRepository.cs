using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Abstract
{
    public interface ICategoryRepository
    {
        Task<Category> AddAsync(Category Category);
        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
        Task<Category> UpdateAsync(Category Category);
        Task<List<Category>> GetByCategoryId(int categoryId);
    }
}
