using InventoryManagement.Core.Entities;

namespace InventoryManagement.Infrastructure.Abstract
{
    public interface IOrderHistoryStore
    {
        void Save(int orderId, OrderMemento memento);
        OrderMemento? GetLast(int orderId);
    }
}
