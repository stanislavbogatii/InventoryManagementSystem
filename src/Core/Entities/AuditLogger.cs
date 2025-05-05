using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class AuditLogger : IEventListener<ProductCreatedEvent>
    {
        public void Handle(ProductCreatedEvent e)
        {
            Console.WriteLine($"[AUDIT] Product created: {e.ProductName} at {e.Timestamp}");
        }
    }
}
