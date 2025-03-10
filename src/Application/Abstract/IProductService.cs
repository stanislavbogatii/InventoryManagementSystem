using InventoryManagement.Core.Entities;
using InventoryManagement.Application.DTOs; 

namespace InventoryManagement.Application.Abstract
{
    public interface IProductService
    {
        Task<Product> CreateProductAsync(ProductDto productDto);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<List<ProductDto>> GetAllProductsAsync();
    }
}
