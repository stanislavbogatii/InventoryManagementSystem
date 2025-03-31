namespace InventoryManagement.Application.Components
{
    public class ProductComponent : InventoryComponent
    {
        public decimal Price { get; private set; }

        public ProductComponent(int id, decimal price) : base(id)
        {
            Price = price;
        }

        public override decimal CalculateTotalCost() => Price;
    }
}
