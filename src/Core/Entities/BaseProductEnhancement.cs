using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class BaseProductEnhancement : IProductEnhancement
    {
        private readonly Product _product;

        public BaseProductEnhancement(Product product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public Product GetBaseProduct()
        {
            return _product;
        }

        public decimal GetEnhancedPrice()
        {
            return _product.Price;
        }

        public string GetDescription()
        {
            return _product.Name;
        }

        public decimal CalculateDiscount()
        {
            return _product.CalculateDiscount();
        }
    }
}
