using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Core.Entities
{
    namespace InventoryManagement.Core.Reports
    {
        public abstract class Report
        {
            public string Title { get; set; }
            public DateTime GeneratedAt { get; set; }
            public abstract string GenerateContent();
        }

        public class InventoryReport : Report
        {
            public List<Product> Products { get; set; }

            public InventoryReport(List<Product> products)
            {
                Products = products;
                Title = "Inventory Report";
                GeneratedAt = DateTime.UtcNow;
            }

            public override string GenerateContent()
            {
                return $"Inventory Report ({GeneratedAt}):\n" +
                       string.Join("\n", Products.Select(p => $"{p.Name}: {p.StockQuantity} units"));
            }
        }

        public class OrderReport : Report
        {
            public List<Order> Orders { get; set; }

            public OrderReport(List<Order> orders)
            {
                Orders = orders;
                Title = "Order Report";
                GeneratedAt = DateTime.UtcNow;
            }

            public override string GenerateContent()
            {
                return $"Order Report ({GeneratedAt}):\n" +
                       string.Join("\n", Orders.Select(o => $"Order #{o.Id}: {o.Status}, Total: {o.TotalAmount}"));
            }
        }
    }
}
