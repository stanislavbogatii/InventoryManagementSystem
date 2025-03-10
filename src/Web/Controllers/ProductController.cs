// src/Web/Controllers/ProductsController.cs
using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.DTOs;
using InventoryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productDto);
            return Ok(MapToDto(createdProduct)); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productService.GetAllProductsAsync();
            return Ok(products);
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