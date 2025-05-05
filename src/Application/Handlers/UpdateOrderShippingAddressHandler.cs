using InventoryManagement.Application.Commands;
using InventoryManagement.Infrastructure.Abstract;

namespace InventoryManagement.Application.Handlers
{
    public class UpdateOrderShippingAddressHandler
    {
        private readonly IOrderRepository _repository;
        private readonly IOrderHistoryStore _historyStore;

        public UpdateOrderShippingAddressHandler(IOrderRepository repository, IOrderHistoryStore historyStore)
        {
            _repository = repository;
            _historyStore = historyStore;
        }

        public async Task HandleAsync(UpdateShippingAddressCommand command)
        {
            var order = await _repository.GetByIdAsync(command.OrderId);

            var snapshot = order.CreateMemento();
            _historyStore.Save(command.OrderId, snapshot);
            order.ShippingAddress = command.NewAddress;
            await _repository.UpdateAsync(order);
        }
    }

}
