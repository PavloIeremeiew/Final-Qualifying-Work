namespace SupermarketStorageSystem.Applications.Services
{
    public interface IInventoryService
    {
        public Task ProcessInventory(string barcode, int actualQuantity, string userId);
        public Task SellProductAsync(string barcode, int quantity, string userId);
        public Task ReceiveProductAsync(string barcode, int quantity, string userId);
        public Task<bool> IsStockLow(string barcode);
    }
}