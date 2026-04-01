using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Data.Context
{
    public interface IApplicationDbContext
    {
        public IQueryable<Product> Products { get; }
        public IQueryable<Category> Categories { get; }
        public IQueryable<InventoryLog> InventoryLogs { get; }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}