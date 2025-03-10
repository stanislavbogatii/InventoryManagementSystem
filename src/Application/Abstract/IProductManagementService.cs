using InventoryManagement.Core.Entities;
using InventoryManagement.Application.DTOs; 

namespace InventoryManagement.Application.Abstract
{
    public interface IProductManagementService
    {
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<Product>> GetAllProductsAsync();
        Task UpdateStockAsync(int productId, int newQuantity);
    }
}
