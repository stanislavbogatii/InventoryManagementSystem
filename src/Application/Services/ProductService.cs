using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Factories;
using InventoryManagement.Application.Security;
using InventoryManagement.Core.Abstract;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;
using InventoryManagement.Core.Enums;
using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Infrastructure.Adapters;
using InventoryManagement.Infrastructure.Services;
using Microsoft.Extensions.Logging;

namespace InventoryManagement.Application.Services
{
    public class ProductService : IProductManagementService, IProductStockService
    {
        private readonly IProductRepository _repository;
        private readonly ProductPropertiesFactory _factory;
        private readonly ICustomLogger _logger;
        private readonly IProductRepository _proxiedRepository;
        private readonly ILogger<ProductAccessControlProxy> _standartLogger;


        public ProductService(IProductRepository repository, ICustomLogger _customLogger, ProductPropertiesFactory factory, ILogger<ProductAccessControlProxy> standartLogger)
        {
            _factory = factory;
            _repository = repository;
            _logger = _customLogger;
            _standartLogger = standartLogger;

            var currentUser = new User
            {
                Id = 1,
                Username = "testAdmin",
                Role = UserRole.Customer
            };

            _proxiedRepository = new ProductAccessControlProxy(_repository, currentUser, _standartLogger);
        }

        public async void DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) throw new Exception("Product not found");
            //await _repository.DeleteAsync(product);
            _logger.Log("Product deleted", LogType.Info);
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


            var dispatcher = new EventDispatcher();
            dispatcher.Subscribe(new AuditLogger());
            var newEvent = new ProductCreatedEvent(product.Name);
            dispatcher.Dispatch(newEvent);

            return await _proxiedRepository.AddAsync(product);
        }

        public async Task<ProductDto> GetProductByIdAsync(int id)
        {
            var product = await _proxiedRepository.GetByIdAsync(id);
            return MapToDto(product);
        }

        public async Task<List<Product>> GetAllProductsAsync()
        {
            var products = await _proxiedRepository.GetAllAsync();
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
            await _proxiedRepository.UpdateAsync(product);


            var emailNotifier = new LowStockNotifier(new EmailNotificationStrategy());
            emailNotifier.NotifyLowStock(product.Name);

            var telegramNotifier = new LowStockNotifier(new TelegramNotificationStrategy());
            telegramNotifier.NotifyLowStock(product.Name);

            var silentNotifier = new LowStockNotifier(new SilentLogNotificationStrategy());
            silentNotifier.NotifyLowStock(product.Name);
        }

        public async Task<IProductEnhancement> ApplyEnhancementsAsync(int productId, bool addGiftWrap = false, decimal? specialDiscountPercentage = null)
        {
            var product = await _proxiedRepository.GetByIdAsync(productId);
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
            var product = await _proxiedRepository.GetByIdAsync(productId);
            IPricingStrategy pricingStrategy;

            switch (pricingStrategyType.ToLower())
            {
                case "standard":
                    pricingStrategy = new StandardPricingStrategy();
                    break;
                case "taxed":
                    pricingStrategy = new TaxedPricingStrategy(strategyParameter ?? 0.20m);
                    break;
                case "regional":
                    pricingStrategy = new RegionalPricingStrategy(strategyParameter ?? 10.00m);
                    break;
                default:
                    throw new ArgumentException($"Unknown pricing strategy: {pricingStrategyType}");
            }

            return new ProductPricing(product, pricingStrategy);
        }
    }
}
