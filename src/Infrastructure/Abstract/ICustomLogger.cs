

using InventoryManagement.Core.Enums;

namespace InventoryManagement.Infrastructure.Abstract
{
    public interface ICustomLogger
    {
        void Log(string message, LogType type);
    }
}
