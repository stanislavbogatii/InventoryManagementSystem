using InventoryManagement.Core.Entities;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Abstract;

namespace InventoryManagement.Application.Abstract
{
    public interface IProductManagementService
    {
        Task<Product> CreateProductAsync(ProductDto productDto);
        void DeleteProductAsync(int id);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task UpdateStockAsync(int productId, int newQuantity);
        Task<IProductEnhancement> ApplyEnhancementsAsync(int productId, bool addGiftWrap = false, decimal? specialDiscountPercentage = null);
    }
}
