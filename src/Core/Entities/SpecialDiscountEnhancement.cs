using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class SpecialDiscountEnhancement : ProductEnhancement
    {
        private readonly decimal _discountPercentage;

        public SpecialDiscountEnhancement(IProductEnhancement product, decimal discountPercentage = 0.10m)
            : base(product)
        {
            if (discountPercentage < 0 || discountPercentage > 1)
                throw new ArgumentException("Discount percentage must be between 0 and 1.");
            _discountPercentage = discountPercentage;
        }

        public override decimal CalculateDiscount()
        {
            decimal basePrice = GetEnhancedPrice();
            return basePrice * _discountPercentage;
        }

        public override string GetDescription()
        {
            return $"{base.GetDescription()} (with discount {(_discountPercentage * 100)}%)";
        }
    }
}
