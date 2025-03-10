namespace InventoryManagement.Core.Entities
{
    namespace InventoryManagement.Core.Reports
    {
        public abstract class Report
        {
            public string Title { get; set; }
            public DateTime GeneratedAt { get; set; }
            //public abstract string GenerateContent();
        }

        public class OrderPdfReport : Report
        {
            public List<Order> Orders { get; set; }

            public OrderPdfReport(List<Order> orders)
            {
                Orders = orders;
                Title = "Order Report";
                GeneratedAt = DateTime.UtcNow;
            }
        }

        public class OrderExcelReport : Report
        {
            public List<Order> Orders { get; set; }

            public OrderExcelReport(List<Order> orders)
            {
                Orders = orders;
                Title = "Order Report";
                GeneratedAt = DateTime.UtcNow;
            }
        }

        public class InventoryPdfReport : Report
        {
            public List<Product> Products { get; set; }

            public InventoryPdfReport(List<Product> products)
            {
                Products = products;
                Title = "Inventory Report";
                GeneratedAt = DateTime.UtcNow;
            }
        }

        public class InventoryExcelReport : Report
        {
            public List<Product> Products { get; set; }

            public InventoryExcelReport(List<Product> products)
            {
                Products = products;
                Title = "Inventory Report";
                GeneratedAt = DateTime.UtcNow;
            }
        }
    }
}
