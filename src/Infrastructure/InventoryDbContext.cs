using InventoryManagement.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace InventoryManagement.Infrastructure
{
    public class InventoryDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<ProductProperties> ProductProperties { get; set; } 

        public InventoryDbContext(DbContextOptions<InventoryDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.ParentCategory)
                .WithMany()
                .HasForeignKey(c => c.ParentCategoryId);

            modelBuilder.Entity<Product>()
                .HasDiscriminator<string>("ProductType")
                .HasValue<ElectronicsProduct>("Electronics")
                .HasValue<FoodProduct>("Food");

            modelBuilder.Entity<Product>()
                .HasOne(i => i.Properties)
                .WithMany()
                .HasForeignKey(i => i.ProductPropertiesId);

            modelBuilder.Entity<ProductProperties>()
                .HasIndex(p => new { p.ModelName, p.Weight, p.Dimensions, p.Color, p.Category })
                .IsUnique();

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany()
                .HasForeignKey(oi => oi.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.Items)
                .HasForeignKey(oi => oi.OrderId);

            modelBuilder.Entity<Warehouse>()
                .HasKey(w => w.Id);

            modelBuilder.Entity<Order>()
                .HasKey(o => o.Id);
            modelBuilder.Entity<Order>()
                .Property(o => o.CustomerName)
                .HasMaxLength(100);
            modelBuilder.Entity<Order>()
                .Property(o => o.CreatedAt)
                .IsRequired();
            modelBuilder.Entity<Order>()
                .Property(o => o.Status)
                .IsRequired();

            modelBuilder.Entity<Warehouse>()
            .Property(w => w.Code)
            .IsRequired()
            .HasMaxLength(50); 

            modelBuilder.Entity<Warehouse>()
                .Property(w => w.Address)
                .IsRequired()
                .HasMaxLength(200); 

            modelBuilder.Entity<Warehouse>()
                .Property(w => w.TotalCapacity)
                .IsRequired();
        }

        public class InventoryDbContextFactory : IDesignTimeDbContextFactory<InventoryDbContext>
        {
            public InventoryDbContext CreateDbContext(string[] args)
            {
                var optionsBuilder = new DbContextOptionsBuilder<InventoryDbContext>();
                optionsBuilder.UseSqlite("Data Source=inventory.db"); 

                return new InventoryDbContext(optionsBuilder.Options);
            }
        }
    }
}
