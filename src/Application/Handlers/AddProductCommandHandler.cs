using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Commands;
using InventoryManagement.Core.Entities;

namespace InventoryManagement.Application.Handlers
{
    public class AddProductCommandHandler : ICommandHandler<AddProductCommand>
    {
        private readonly IProductManagementService _service;

        public AddProductCommandHandler(IProductManagementService service)
        {
            _service = service;
        }

        public async Task HandleAsync(AddProductCommand command)
        {
            var product = new Core.DTOs.ProductDto(
                command.Name,
                command.Price,
                command.Quantity
            );
            await _service.CreateProductAsync(product);
        }
    }
}
