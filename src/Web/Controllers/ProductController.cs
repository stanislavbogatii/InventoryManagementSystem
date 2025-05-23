﻿// src/Web/Controllers/ProductsController.cs
using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Commands;
using InventoryManagement.Core.DTOs;
using InventoryManagement.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace InventoryManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductManagementService _productMangementService;
        private readonly IProductStockService _productStockService;
        private readonly ICommandHandler<AddProductCommand> _addProductHandler;

        public ProductsController(IProductManagementService productMangementService, IProductStockService productStockService, ICommandHandler<AddProductCommand> addProductHandler)
        {
            _productMangementService = productMangementService;
            _productStockService = productStockService;
            _addProductHandler = addProductHandler;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] ProductDto productDto)
        {
            var createdProduct = await _productMangementService.CreateProductAsync(productDto);
            var command = new AddProductCommand
                (productDto.Name, productDto.Price, productDto.StockQuantity);
  
            await _addProductHandler.HandleAsync(command);
            return Ok(MapToDto(createdProduct));
        }


        [HttpPost("/{id}/enhancements")]
        public async Task<IActionResult> ApplyEnhancements(int id, [FromBody] EnhacementDto enhacementDto)
        {
            var createdProduct = await _productMangementService.ApplyEnhancementsAsync(id, enhacementDto.AddGiftWrap, enhacementDto.SpecialDiscountPercentage);
            return Ok("Saved");
        }




        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _productMangementService.GetProductByIdAsync(id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _productMangementService.GetAllProductsAsync();
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


public class EnhacementDto
{
    public bool AddGiftWrap { get; set; }
    public decimal? SpecialDiscountPercentage { get; set; }
}