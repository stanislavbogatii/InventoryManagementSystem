namespace InventoryManagement.Core.Abstract
{
    public interface IInventoryEvent
    {
        string Name { get; }
        DateTime Timestamp { get; }
    }

}
