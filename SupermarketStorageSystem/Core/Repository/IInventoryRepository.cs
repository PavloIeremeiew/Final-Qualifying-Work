using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Core.Repository
{
    public interface IInventoryRepository
    {
        Task<Product> GetByBarcodeAsync(string barcode);
        Task UpdateProductAsync(Product product);
        Task AddLogAsync(InventoryLog log);
        Task SaveChangesAsync();
    }
}