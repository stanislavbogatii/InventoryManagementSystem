using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class StandardPricingStrategy : IPricingStrategy
    {
        public decimal CalculatePrice(Product product)
        {
            return product.Price;
        }
    }
    
    public class TaxedPricingStrategy : IPricingStrategy
    {
        private readonly decimal _taxRate;

        public TaxedPricingStrategy(decimal taxRate)
        {
            if (taxRate < 0)
                throw new ArgumentException("Tax rate cannot be negative.");
            _taxRate = taxRate;
        }

        public decimal CalculatePrice(Product product)
        {
            return product.Price * (1 + _taxRate);
        }
    }

    public class RegionalPricingStrategy : IPricingStrategy
    {
        private readonly decimal _regionalAdjustment;

        public RegionalPricingStrategy(decimal regionalAdjustment)
        {
            _regionalAdjustment = regionalAdjustment;
        }

        public decimal CalculatePrice(Product product)
        {
            return product.Price + _regionalAdjustment;
        }

    }
}
