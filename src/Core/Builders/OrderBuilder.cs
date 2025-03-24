using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Builders
{
    public class OrderBuilder
    {
        private int Id;
        private string CustomerName;
        private DateTime CreatedAt;
        private OrderStatus Status;
        private List<OrderItem> Items = new List<OrderItem>();

        public OrderBuilder SetOrderId(int id)
        {
            Id = id;
            return this;
        }

        public OrderBuilder SetCustomerName(string customerName)
        {
            CustomerName = customerName;
            return this;
        }

        public OrderBuilder SetOrderDate(DateTime createdAt)
        {
            CreatedAt = createdAt;
            return this;
        }

        public OrderBuilder AddProduct(OrderItem item)
        {
            Items.Add(item);
            return this;
        }

        public OrderBuilder SetStatus(OrderStatus status)
        {
            Status = status;
            return this;
        }
        public Order Build()
        {
            return new Order(Id, CustomerName, CreatedAt, Status, Items);
        }
    }
}
