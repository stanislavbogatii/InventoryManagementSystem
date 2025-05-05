namespace InventoryManagement.Core.Abstract
{
    public interface IEventListener<in T> where T : IInventoryEvent
    {
        void Handle(T inventoryEvent);
    }

}
