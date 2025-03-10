using InventoryManagement.Core.Enums;

namespace InventoryManagement.Core.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public OrderStatus Status { get; set; }
        public List<OrderItem> Items { get; set; } = new List<OrderItem>();

        public decimal TotalAmount => Items.Sum(item => item.Quantity * item.UnitPrice);
    }
}
