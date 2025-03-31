using InventoryManagement.Application.Abstract;

namespace InventoryManagement.Application.Components
{
    public abstract class InventoryComponent
    {
        public int Id { get; protected set; }
        protected InventoryComponent(int id) { Id = id; }
        public abstract decimal CalculateTotalCost();
    }
}
