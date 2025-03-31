using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Facades;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;


namespace InventoryManagement.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomLogger _logger;

        public OrderService(IOrderRepository orderRepository, IProductRepository productRepository, ICustomLogger logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<OrderDto> CreateOrderAsync(OrderDto orderDto)
        {
            OrderFacade facade = new OrderFacade(_orderRepository, _productRepository, _logger);
            Order createdOrderDto = await facade.CreateOrderAsync(orderDto);
            return MapToDto(createdOrderDto);
        }

        public async Task<OrderDto> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepository.GetByIdAsync(id);
            return MapToDto(order);
        }

        public async Task<List<Order>> GetAllOrdersAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return orders;
        }

        private OrderDto MapToDto(Order order)
        {
            if (order == null) return null;

            return new OrderDto
            {
                Id = order.Id,
                CreatedAt = order.CreatedAt,
                Status = order.Status.ToString(),
                Items = order.Items.Select(item => new OrderItemDto
                {
                    Id = item.Id,
                    ProductId = item.ProductId,
                    ProductName = item.Product.Name,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice
                }).ToList(),
                TotalAmount = order.TotalAmount
            };
        }
    }
}
