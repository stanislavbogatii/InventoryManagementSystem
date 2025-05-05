namespace InventoryManagement.Core.Entities
{
    public class OrderMemento
    {
        public int OrderId { get; }
        public string ShippingAddress { get; }

        public OrderMemento(int orderId, string shippingAddress)
        {
            OrderId = orderId;
            ShippingAddress = shippingAddress;
        }
    }

}
