

namespace InventoryManagement.Application.DTOs
{
    public class OrderDto
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } // Используем строку для простоты в API
        public List<OrderItemDto> Items { get; set; } = new List<OrderItemDto>();
        public decimal TotalAmount { get; set; }
    }
}
