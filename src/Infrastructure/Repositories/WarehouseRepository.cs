using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Infrastructure.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private InventoryDbContext _context;
        public WarehouseRepository(InventoryDbContext context)
        {
            _context = context;
        }
        public async Task<Warehouse> AddAsync(Warehouse warehouse)
        {
            _context.Warehouses.Add(warehouse);
            await _context.SaveChangesAsync();
            return warehouse;
        }

        public async Task<List<Warehouse>> GetAllAsync()
        {
            List<Warehouse> warehouses = _context.Warehouses
                .ToList();
            return warehouses;
        }

        public async Task<Warehouse> GetByIdAsync(int id)
        {
            Warehouse warehouse = _context.Warehouses
                .FirstOrDefault(w => w.Id == id);
            return warehouse;
        }
    }
}
