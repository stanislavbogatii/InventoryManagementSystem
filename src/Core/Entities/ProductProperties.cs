namespace InventoryManagement.Core.Entities
{
    public class ProductProperties
    {
        public int Id { get; set; }
        public string? ModelName { get; }
        public double? Weight { get; }
        public string? Dimensions { get; }
        public string? Color { get; }
        public string? Category { get; }

        public ProductProperties(string? modelName, double? weight, string? dimensions, string? color, string? category)
        {
            ModelName = modelName;
            Weight = weight;
            Dimensions = dimensions;
            Color = color;
            Category = category;
        }
    }
}
