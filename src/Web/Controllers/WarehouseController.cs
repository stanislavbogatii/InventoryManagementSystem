using InventoryManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;
using InventoryManagement.Application.Abstract;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WarehouseController: ControllerBase
    {
        private IWarehouseService _warehouseService;
        public WarehouseController(IWarehouseService warehouseService)
        {
            _warehouseService = warehouseService;
        }


        [HttpPost("create-by-prototype")]
        public async Task<IActionResult> CreateWarehouseByPrototype([FromQuery] int prototypeId, [FromBody] WarehouseDto dto)
        {
            Warehouse newWarehouse = await _warehouseService.CreateWarehouseFromPrototypeAsync(prototypeId, dto.Code, dto.Address);
            return Ok(newWarehouse);
        }

        [HttpPost()]
        public async Task<IActionResult> CreateWarehouse([FromBody] WarehouseDto dto)
        {
            Warehouse newWarehouse = await _warehouseService.CreateAsync(dto);
            return Ok(newWarehouse);
        }

        [HttpGet()]
        public async Task<IActionResult> GetAllWarehouses()
        {
            var warehouses = await _warehouseService.GetAllAsync();
            return Ok(warehouses);
        }

    }
}
