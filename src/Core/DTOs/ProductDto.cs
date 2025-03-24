namespace InventoryManagement.Core.DTOs
{
    public class ProductDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductType { get; set; }
        public string? WarrantyPeriod { get; set; }
        public DateTime? ExpirationDate { get; set; }
    }
}
