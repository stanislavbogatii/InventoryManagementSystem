using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }

        public string? CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalAmount => Items.Sum(item => item.Quantity * item.UnitPrice);

        public Order()
        {
            Items = new List<OrderItem>(); 
        }

        public Order(int id, string customerName, DateTime createdAt, OrderStatus status, List<OrderItem> items)
        {
            Id = id;
            CustomerName = customerName;
            CreatedAt = createdAt;
            Status = status;
            Items = items;
        }
    }
}
