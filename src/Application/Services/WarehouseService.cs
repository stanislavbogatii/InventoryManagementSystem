using InventoryManagement.Application.Abstract;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Application.Services
{
    public class WarehouseService : IWarehouseService
    {
        private readonly IWarehouseRepository _repository;

        public WarehouseService(IWarehouseRepository repository)
        {
            _repository = repository;
        }

        public async Task<Warehouse> CreateWarehouseFromPrototypeAsync(int prototypeId, string newCode, string newAddress)
        {
            IPrototype<Warehouse> prototype = await _repository.GetByIdAsync(prototypeId);
            if (prototype == null)
                throw new Exception("Error: Prototype not found");

            var newWarehouse = prototype.Clone();
            newWarehouse.Code = newCode;
            newWarehouse.Address = newAddress;

            await _repository.AddAsync(newWarehouse);
            return newWarehouse;
        }

        public async Task<Warehouse> CreateAsync(WarehouseDto warehouseDto)
        {
            var warehouse = new Warehouse(warehouseDto.Code, warehouseDto.Address, warehouseDto.TotalCapacity);
            await _repository.AddAsync(warehouse);
            return warehouse;
        }

        public async Task<List<Warehouse>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }
    }
}
