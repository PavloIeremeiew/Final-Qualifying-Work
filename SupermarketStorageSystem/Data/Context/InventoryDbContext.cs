using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Data.Context
{
    public class InventoryDbContext : DbContext
    {
        public InventoryDbContext(DbContextOptions<InventoryDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }
}