using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Data.Context
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        IQueryable<Product> IApplicationDbContext.Products => Products;

        IQueryable<Category> IApplicationDbContext.Categories => Categories;

        IQueryable<InventoryLog> IApplicationDbContext.InventoryLogs => InventoryLogs;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }
}