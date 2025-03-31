using InventoryManagement.Core.Entities;

namespace InventoryManagement.Application.Abstract
{
    public interface ICategoryManagement
    {
        Task<Category> CreateAsync(Category category);
        Task<Category> GetByIdAsync(int id);
        Task<List<Category>> GetAllAsync();
    }
}
