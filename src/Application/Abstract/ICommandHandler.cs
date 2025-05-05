namespace InventoryManagement.Application.Abstract
{
    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command);
    }
}
    