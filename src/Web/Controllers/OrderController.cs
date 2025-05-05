// src/Web/Controllers/OrdersController.cs
using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Handlers;
using InventoryManagement.Core.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly UpdateOrderShippingAddressHandler _updateHandler;
        private readonly UndoOrderChangesHandler _undoHandler;

        public OrdersController(IOrderService orderService, UpdateOrderShippingAddressHandler updateHandler, UndoOrderChangesHandler undoHandler)
        {
            _orderService = orderService;
            _updateHandler = updateHandler;
            _undoHandler = undoHandler;
        }

        [HttpPut("{id}/address")]
        public async Task<IActionResult> UpdateAddress(int id, [FromBody] string newAddress)
        {
            await _updateHandler.HandleAsync(new UpdateShippingAddressCommand { OrderId = id, NewAddress = newAddress });
            return Ok("Updated.");
        }

        [HttpPut("{id}/undo")]
        public async Task<IActionResult> Undo(int id)
        {
            await _undoHandler.HandleAsync(id);
            return Ok("Undo complete.");
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder([FromBody] OrderDto orderDto)
        {
            var createdOrder = await _orderService.CreateOrderAsync(orderDto);
            return Ok(createdOrder);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return order != null ? Ok(order) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderService.GetAllOrdersAsync();
            return Ok(orders);
        }
    }
}