namespace InventoryManagement.Application.Components
{
    public class CategoryComponent : InventoryComponent
    {
        private List<InventoryComponent> _components = new List<InventoryComponent>();

        public CategoryComponent(int id) : base(id) { }

        public void Add(InventoryComponent component) => _components.Add(component);
        public void Remove(InventoryComponent component) => _components.Remove(component);

        public override decimal CalculateTotalCost()
        {
            return _components.Sum(c => c.CalculateTotalCost());
        }

    }
}
