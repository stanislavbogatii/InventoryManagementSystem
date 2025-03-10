using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryManagement.Application.DTOs
{
    public class ProductDto
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string ProductType { get; set; } 
        public string? WarrantyPeriod { get; set; } 
        public DateTime? ExpirationDate { get; set; } 
    }
}
