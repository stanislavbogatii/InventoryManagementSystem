
namespace InventoryManagement.Core.Entities
{
    public abstract class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public DateTime LastUpdated { get; set; }
        public Product(string name, decimal price, int stockQuantity)
        {
            Name = name;
            Price = price;
            StockQuantity = stockQuantity;
            LastUpdated = DateTime.UtcNow;
        }

        public virtual decimal CalculateDiscount() => 0m;
    }
    public class ElectronicsProduct : Product
    {
        public string WarrantyPeriod { get; set; }

        public ElectronicsProduct(string name, decimal price, int stockQuantity, string warrantyPeriod)
            : base(name, price, stockQuantity)
        {
            WarrantyPeriod = warrantyPeriod;
        }

        public override decimal CalculateDiscount()
        {
            return StockQuantity > 10 ? Price * 0.1m : 0m;
        }
    }

    public class FoodProduct : Product
    {

        public DateTime? ExpirationDate { get; set; }

        public FoodProduct(string name, decimal price, int stockQuantity, DateTime? expirationDate)
            : base(name, price, stockQuantity)
        {
            ExpirationDate = expirationDate;
        }


        public override decimal CalculateDiscount()
        {

            decimal discount = ExpirationDate.HasValue && (ExpirationDate.Value - DateTime.UtcNow).Days < 7 ? Price * 0.3m : 0m;
            return discount;
        }
    }
}
