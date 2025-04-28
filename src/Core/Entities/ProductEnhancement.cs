using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public abstract class ProductEnhancement : IProductEnhancement
    {
        protected readonly IProductEnhancement _product;

        protected ProductEnhancement(IProductEnhancement product)
        {
            _product = product ?? throw new ArgumentNullException(nameof(product));
        }

        public virtual Product GetBaseProduct()
        {
            return _product.GetBaseProduct();
        }

        public virtual decimal GetEnhancedPrice()
        {
            return _product.GetEnhancedPrice();
        }

        public virtual string GetDescription()
        {
            return _product.GetDescription();
        }

        public virtual decimal CalculateDiscount()
        {
            return _product.CalculateDiscount();
        }
    }

}
