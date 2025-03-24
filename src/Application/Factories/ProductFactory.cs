using InventoryManagement.Core.Entities;
using InventoryManagement.Core.DTOs;

namespace InventoryManagement.Application.Factories
{
    public abstract class ProductFactory()
    {
        public abstract Product CreateProduct(ProductDto productDto);
    }

    public class ElectronicsProductFactory : ProductFactory
    {
        public override Product CreateProduct(ProductDto productDto)
        {
            return new ElectronicsProduct(productDto.Name, productDto.Price, productDto.StockQuantity, productDto.WarrantyPeriod);
        }
    }

    public class FoodProductFactory : ProductFactory
    {
        public override Product CreateProduct(ProductDto productDto)
        {
            return new FoodProduct(productDto.Name, productDto.Price, productDto.StockQuantity, productDto.ExpirationDate);

        }
    }
}
