using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Abstract
{
    public interface IWarehouseRepository
    {
        public Task<Warehouse> AddAsync(Warehouse warehouse);
        public Task<List<Warehouse>> GetAllAsync();
        public Task<Warehouse> GetByIdAsync(int id);
    }
}
