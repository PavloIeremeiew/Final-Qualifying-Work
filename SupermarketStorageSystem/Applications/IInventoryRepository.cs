using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Applications
{
    public interface IInventoryRepository
    {
        public Task<Product> GetByBarcodeAsync(string barcode);
        public Task<Product> GetByIdAsync(int id);
        public Task UpdateProductAsync(Product product);
        public Task AddLogAsync(InventoryLog log);
        public Task SaveChangesAsync();
        public Task<IEnumerable<Product>> GetAllAsync();
    }
}