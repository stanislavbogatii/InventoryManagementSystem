using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Commands;
using InventoryManagement.Application.Handlers;
using InventoryManagement.Application.Services;
using InventoryManagement.Infrastructure;
using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Infrastructure.Repositories;
using InventoryManagement.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductManagementService, ProductService>();
builder.Services.AddScoped<IProductStockService, ProductService>();
builder.Services.AddScoped<IProductPropertiesRepository, ProductPropertiesRepository>();


builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IWarehouseService, WarehouseService>();
builder.Services.AddScoped<IWarehouseRepository, WarehouseRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<ICategoryComposite, CategoryService>();

builder.Services.AddScoped<IProductRepository, IProductRepository>();
builder.Services.AddScoped<ICommandHandler<AddProductCommand>, AddProductCommandHandler>();


builder.Services.AddScoped<ProductPropertiesFactory>();





builder.Services.AddSingleton<ICustomLogger>(CustomLogger.Instance);

builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Inventory Management API",
        Version = "v1",
        Description = "API для управления инвентарём и заказами малого бизнеса"
    });
});

var app = builder.Build();

app.UseHttpsRedirection();
app.UseAuthorization();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Management API v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();