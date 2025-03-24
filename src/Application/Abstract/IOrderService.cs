using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Application.Abstract
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<List<Order>> GetAllOrdersAsync();
    }
}
