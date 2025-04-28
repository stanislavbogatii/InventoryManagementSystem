using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Core.Entities
{
    public class GiftWrapEnhancement : ProductEnhancement
    {
        private readonly decimal _giftWrapCost;

        public GiftWrapEnhancement(IProductEnhancement product, decimal giftWrapCost = 5.00m)
            : base(product)
        {
            _giftWrapCost = giftWrapCost;
        }

        public override decimal GetEnhancedPrice()
        {
            return base.GetEnhancedPrice() + _giftWrapCost;
        }

        public override string GetDescription()
        {
            return $"{base.GetDescription()}";
        }
    }
}
