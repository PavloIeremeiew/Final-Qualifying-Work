using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Entities.Security;

namespace SupermarketStorageSystem.Applications
{
    public interface IApplicationDbContext
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Category> Categories { get; }
        public IQueryable<InventoryLog> InventoryLogs { get; }


        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
        public void UpdateProduct(Product product);
        public Task AddLogAsync(InventoryLog log, CancellationToken cancellationToken = default);
        public Task AddProductAsync(Product product);
        public Task AddCategoryAsync(Category category);
    }
}