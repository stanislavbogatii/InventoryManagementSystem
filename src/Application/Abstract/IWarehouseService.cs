using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Application.Abstract
{
    public interface IWarehouseService
    {
        public Task<Warehouse> CreateWarehouseFromPrototypeAsync(int prototypeId, string newCode, string newAddress);
        public Task<Warehouse> CreateAsync(WarehouseDto warehouseDto);
        public Task<List<Warehouse>> GetAllAsync();
    }
}
