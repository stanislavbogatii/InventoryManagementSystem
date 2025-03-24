using InventoryManagement.Application.Abstract;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Builders;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
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
            try
            {
                OrderBuilder orderBuilder = new OrderBuilder()
                         .SetCustomerName(orderDto.CustomerName)
                         .SetOrderDate(orderDto.CreatedAt)
                         .SetStatus(Enum.Parse<OrderStatus>(orderDto.Status));

                foreach (var itemDto in orderDto.Items)
                {
                    var product = _productRepository.GetByIdAsync(itemDto.ProductId).Result;
                    if (product == null) throw new ArgumentException($"Product with ID {itemDto.ProductId} not found");

                    orderBuilder.AddProduct(new OrderItem
                    {
                        ProductId = itemDto.ProductId,
                        Quantity = itemDto.Quantity,
                        UnitPrice = product.Price
                    });
                }
                Order order = orderBuilder.Build();
                Order createdOrder = await _orderRepository.AddAsync(order);

                _logger.Log("Order created successfully", LogType.Info);
                return MapToDto(createdOrder);
            }
            catch (Exception ex)
            {
                _logger.Log($"Error creating order: {ex.Message}", LogType.Error);
                throw;
            }
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
