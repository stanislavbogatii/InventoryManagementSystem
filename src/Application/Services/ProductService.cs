using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Factories;
using InventoryManagement.Core.Abstract;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Infrastructure.Adapters;
using InventoryManagement.Infrastructure.Services;

namespace InventoryManagement.Application.Services
{
    public class ProductService : IProductManagementService, IProductStockService
    {
        private readonly IProductRepository _repository;
        private readonly ProductPropertiesFactory _factory;
        private readonly ICustomLogger _logger;

        public ProductService(IProductRepository repository, ICustomLogger _customLogger, ProductPropertiesFactory factory)
        {
            _factory = factory;
            _repository = repository;
            _logger = _customLogger;
        }

        public async Task<Product> CreateProductAsync(ProductDto productDto)
        {
            ProductFactory factory = productDto.ProductType switch
            {
                "Electronics" => new ElectronicsProductFactory(),
                "Food" => new FoodProductFactory(),
                _ => throw new ArgumentException("Invalid product type")
            };

            var properties = await _factory.GetItemPropertiesAsync(
                productDto.Properties.ModelName,
                productDto.Properties.Weight,
                productDto.Properties.Dimensions,
                productDto.Properties.Color,
                productDto.Properties.Category
            );

            Product product = factory.CreateProduct(productDto);
            _logger.Log("Product created", LogType.Info);
            product.Properties = properties;
            FileLogger fileLogger = new FileLogger();
            ICustomLogger fileLoggerAdapter = new FileLoggerAdapter(fileLogger);
            ICustomLogger[] loggers = { _logger, fileLoggerAdapter };

            foreach (ICustomLogger logger in loggers)
            {
                logger.Log("Product created", LogType.Info);
            }
            return await _repository.AddAsync(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            return MapToDto(product);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products;
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

        public async Task UpdateStockAsync(int productId, int newQuantity)
        {
            var product = await _repository.GetByIdAsync(productId); ;
            if (product == null) throw new Exception("Product not found");
            product.StockQuantity = newQuantity;
            product.LastUpdated = DateTime.UtcNow;
            await _repository.UpdateAsync(product);
        }

        public async Task<IProductEnhancement> ApplyEnhancementsAsync(int productId, bool addGiftWrap = false, decimal? specialDiscountPercentage = null)
        {
            var product = await _repository.GetByIdAsync(productId);
            IProductEnhancement enhancedProduct = new BaseProductEnhancement(product);

            if (addGiftWrap)
            {
                enhancedProduct = new GiftWrapEnhancement(enhancedProduct);
            }

            if (specialDiscountPercentage.HasValue)
            {
                enhancedProduct = new SpecialDiscountEnhancement(enhancedProduct, specialDiscountPercentage.Value);
            }

            return enhancedProduct;
        }


        public async Task<ProductPricing> GetProductPricingAsync(int productId, string pricingStrategyType, decimal? strategyParameter = null)
        {
            var product = await _repository.GetByIdAsync(productId);
            IPricingStrategy pricingStrategy;

            switch (pricingStrategyType.ToLower())
            {
                case "standard":
                    pricingStrategy = new StandardPricingStrategy();
                    break;
                case "taxed":
                    pricingStrategy = new TaxedPricingStrategy(strategyParameter ?? 0.20m); // Налог по умолчанию 20%
                    break;
                case "regional":
                    pricingStrategy = new RegionalPricingStrategy(strategyParameter ?? 10.00m); // Корректировка по умолчанию +10
                    break;
                default:
                    throw new ArgumentException($"Unknown pricing strategy: {pricingStrategyType}");
            }

            return new ProductPricing(product, pricingStrategy);
        }
    }
}
