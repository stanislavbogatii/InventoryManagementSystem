using InventoryManagement.Application.Abstract;
using InventoryManagement.Application.Services;
using InventoryManagement.Infrastructure;
using InventoryManagement.Infrastructure.Abstract;
using InventoryManagement.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Добавление сервисов в DI
builder.Services.AddControllers();
builder.Services.AddDbContext<InventoryDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductManagementService, ProductService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IReportService, ReportService>();

// Добавление Swagger
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

// Настройка pipeline
app.UseHttpsRedirection();
app.UseAuthorization();

// Подключение Swagger
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory Management API v1");
    c.RoutePrefix = string.Empty;
});

app.MapControllers();

app.Run();