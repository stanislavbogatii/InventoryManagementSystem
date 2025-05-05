namespace InventoryManagement.Application.Commands
{
    public class UpdateShippingAddressCommand
    {
        public int OrderId { get; set; }
        public string NewAddress { get; set; }
    }
}
