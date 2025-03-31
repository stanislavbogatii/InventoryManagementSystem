namespace InventoryManagement.Application.Abstract
{
    public interface IInventoryComponent
    {
        int Id { get; }
        string Name { get; }
        decimal CalculateTotalCost();
        void DisplayStructure(int depth = 0);
    }
}
