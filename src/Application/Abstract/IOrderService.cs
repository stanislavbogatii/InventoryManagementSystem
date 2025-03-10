using InventoryManagement.Application.DTOs;

namespace InventoryManagement.Application.Abstract
{
    public interface IOrderService
    {
        Task<OrderDto> CreateOrderAsync(OrderDto orderDto);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<List<OrderDto>> GetAllOrdersAsync();
    }
}
