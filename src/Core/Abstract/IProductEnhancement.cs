using InventoryManagement.Core.Entities;

namespace InventoryManagement.Core.Abstract
{
    public interface IProductEnhancement
    {
        public Product GetBaseProduct();

        public decimal GetEnhancedPrice();

        string GetDescription();

        public decimal CalculateDiscount();
    }
}
