using SupermarketStorageSystem.Entities.DTOs;

namespace SupermarketStorageSystem.Applications.Services
{
    public interface IInventoryService
    {
        public Task<LogDTO> ProcessInventory(string barcode, int actualQuantity, string userId);
        public Task<LogDTO> SellProductAsync(string barcode, int quantity, string userId);
        public Task<LogDTO> ReceiveProductAsync(string barcode, int quantity, string userId);
        public Task<bool> IsStockLow(string barcode);
        public Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync();
    }
}