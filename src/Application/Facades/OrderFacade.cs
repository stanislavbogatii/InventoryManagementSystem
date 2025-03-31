using InventoryManagement.Application.Abstract;
using InventoryManagement.Core.Builders;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Application.Facades
{
    public class OrderFacade
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IProductRepository _productRepository;
        private readonly ICustomLogger _logger;

        public OrderFacade(IOrderRepository orderRepository, IProductRepository productRepository, ICustomLogger logger)
        {
            _orderRepository = orderRepository;
            _productRepository = productRepository;
            _logger = logger;
        }

        public async Task<Order> CreateOrderAsync(OrderDto orderDto)
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
                return createdOrder;
            }
            catch (Exception ex)
            {
                _logger.Log($"Error creating order: {ex.Message}", LogType.Error);
                throw;
            }
        }
    }
}
