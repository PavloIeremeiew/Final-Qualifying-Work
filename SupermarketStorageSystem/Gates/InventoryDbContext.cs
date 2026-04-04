using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Gates
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }

        IQueryable<Product> IApplicationDbContext.Products => Products;

        IQueryable<Category> IApplicationDbContext.Categories => Categories;

        IQueryable<InventoryLog> IApplicationDbContext.InventoryLogs => InventoryLogs;

        public void UpdateProduct(Product product)
        {
            Products.Update(product);
        }

        public async Task AddLogAsync(InventoryLog log, CancellationToken cancellationToken = default)
        {
            await InventoryLogs.AddAsync(log, cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.PurchasePrice).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);
        }
    }
}