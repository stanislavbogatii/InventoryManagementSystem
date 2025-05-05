using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class EmailNotificationStrategy : INotificationStrategy
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[EMAIL] {message}");
        }
    }

    public class SmsNotificationStrategy : INotificationStrategy
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[SMS] {message}");
        }
    }

    public class TelegramNotificationStrategy : INotificationStrategy
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[TELEGRAM] {message}");
        }
    }

    public class SilentLogNotificationStrategy : INotificationStrategy
    {
        public void Notify(string message)
        {
            Console.WriteLine($"[LOG ONLY] {message}");
        }
    }

}
