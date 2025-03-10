using InventoryManagement.Application.Abstract;
using InventoryManagement.Core.Entities;
using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Application.Factories;

namespace InventoryManagement.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            ProductFactory factory = productDto.ProductType switch
            {
                "Electronics" => new ElectronicsProductFactory(),
                "Food" => new FoodProductFactory(),
                _ => throw new ArgumentException("Invalid product type")
            };
            Product product = factory.CreateProduct(productDto);
            return await _repository.AddAsync(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return MapToDto(product);
        }

        public async Task<List<ProductDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(MapToDto).ToList();
        }

        private ProductDto MapToDto(Product product)
        {
            if (product == null) return null;

            return product switch
            {
                ElectronicsProduct electronics => new ProductDto
                {
                    Name = electronics.Name,
                    Price = electronics.Price,
                    StockQuantity = electronics.StockQuantity,
                    ProductType = "Electronics",
                    WarrantyPeriod = electronics.WarrantyPeriod
                },
                FoodProduct food => new ProductDto
                {
                    Name = food.Name,
                    Price = food.Price,
                    StockQuantity = food.StockQuantity,
                    ProductType = "Food",
                    ExpirationDate = food.ExpirationDate
                },
                _ => throw new ArgumentException("Unknown product type")
            };
        }


    }
}
