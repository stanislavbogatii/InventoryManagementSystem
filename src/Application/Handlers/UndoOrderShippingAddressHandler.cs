using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Application.Handlers
{
    public class UndoOrderChangesHandler
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderHistoryStore _historyStore;

        public UndoOrderChangesHandler(IOrderRepository repository, IOrderHistoryStore historyStore)
        {
            _repository = repository;
            _historyStore = historyStore;
        }

        public async Task HandleAsync(int orderId)
        {
            var memento = _historyStore.GetLast(orderId);
            if (memento == null)
                return;

            var order = await _repository.GetByIdAsync(orderId);
            order.RestoreMemento(memento);

            await _repository.UpdateAsync(order);
        }
    }
}
