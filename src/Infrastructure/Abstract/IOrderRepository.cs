using InventoryManagement.Core.Entities;


namespace InventoryManagement.Infrastructure.Abstract
{
    public interface IOrderRepository
    {
        Task<Order> AddAsync(Order order);
        Task<Order> GetByIdAsync(int id);
        Task<List<Order>> GetAllAsync();
    }
}
