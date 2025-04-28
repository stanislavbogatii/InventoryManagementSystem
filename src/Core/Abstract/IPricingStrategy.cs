using InventoryManagement.Core.Entities;

namespace InventoryManagement.Core.Abstract
{
    public interface IPricingStrategy
    {
        public decimal CalculatePrice(Product product);
    }
}