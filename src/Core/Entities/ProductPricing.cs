using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class ProductPricing
    {
        private readonly Product _product; 
        private IPricingStrategy _pricingStrategy;

        public ProductPricing(Product product, IPricingStrategy pricingStrategy)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
            _pricingStrategy = pricingStrategy ?? throw new ArgumentNullException(nameof(pricingStrategy));
        }

        public void SetPricingStrategy(IPricingStrategy pricingStrategy)
        {
            _pricingStrategy = pricingStrategy ?? throw new ArgumentNullException(nameof(pricingStrategy));
        }

        public decimal GetPrice()
        {
            return _pricingStrategy.CalculatePrice(_product);
        }

        public Product GetProduct()
        {
            return _product;
        }
    }
}
