
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Abstract;
public class CustomLogger : ICustomLogger
{
    private static CustomLogger _instance;
    private static readonly object _lock = new object();

    private CustomLogger() { }

    public static CustomLogger Instance
    {
        get
        {
            lock (_lock)
            {
                if (_instance == null)
                {
                    _instance = new CustomLogger();
                }
                return _instance;
            }
        }
    }

    public void Log(string message, LogType type)
    {
        Console.WriteLine($"[{DateTime.Now}] {message}");
    }
}