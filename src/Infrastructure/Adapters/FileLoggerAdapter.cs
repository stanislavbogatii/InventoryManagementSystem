using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Infrastructure.Services;

namespace InventoryManagement.Infrastructure.Adapters
{
    public class FileLoggerAdapter : ICustomLogger
    {
        private readonly FileLogger _fileLogger;

        public FileLoggerAdapter(FileLogger fileLogger)
        {
            _fileLogger = fileLogger;
        }
        public void Log(string message, LogType type)
        {
            _fileLogger.WriteToFile($"[{type}] {message}");
        }
    }
}
