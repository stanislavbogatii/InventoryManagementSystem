using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class ProductCreatedEvent : IInventoryEvent
    {
        public string ProductName { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public string Name => "ProductCreated";

        public ProductCreatedEvent(string productName)
        {
            ProductName = productName;
        }
    }
}
