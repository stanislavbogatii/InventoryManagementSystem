using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Infrastructure.Stores
{
    public class InMemoryOrderHistoryStore : IOrderHistoryStore
    {
        private readonly Dictionary<int, Stack<OrderMemento>> _history = new();

        public void Save(int orderId, OrderMemento memento)
        {
            if (!_history.ContainsKey(orderId))
                _history[orderId] = new Stack<OrderMemento>();

            _history[orderId].Push(memento);
        }

        public OrderMemento? GetLast(int orderId)
        {
            if (_history.TryGetValue(orderId, out var stack) && stack.Count > 0)
                return stack.Pop();

            return null;
        }
    }
}
