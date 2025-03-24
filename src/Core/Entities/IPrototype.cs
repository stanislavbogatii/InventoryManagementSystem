namespace InventoryManagement.Core.Entities
{
    public interface IPrototype<T>
    {
        T Clone();
    }
}
