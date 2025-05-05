using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class EventDispatcher
    {
        private readonly Dictionary<Type, List<object>> _subscribers = new();

        public void Subscribe<T>(IEventListener<T> listener) where T : IInventoryEvent
        {
            var type = typeof(T);
            if (!_subscribers.ContainsKey(type))
                _subscribers[type] = new List<object>();

            _subscribers[type].Add(listener);
        }

        public void Dispatch<T>(T inventoryEvent) where T : IInventoryEvent
        {
            var type = typeof(T);
            if (_subscribers.TryGetValue(type, out var listeners))
            {
                foreach (var listener in listeners.Cast<IEventListener<T>>())
                {
                    listener.Handle(inventoryEvent);
                }
            }
        }
    }
}
