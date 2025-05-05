using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class LowStockNotifier
    {
        private readonly INotificationStrategy _strategy;

        public LowStockNotifier(INotificationStrategy strategy)
        {
            _strategy = strategy;
        }

        public void NotifyLowStock(string productName)
        {
            string message = $"Product '{productName}' is low in stock!";
            _strategy.Notify(message);
        }
    }
}
