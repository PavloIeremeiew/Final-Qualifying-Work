namespace SupermarketStorageSystem.Applications.Services
{
    public interface IInventoryService
    {
        public Task ProcessInventory(string barcode, int actualQuantity, int userId);
        public Task SellProductAsync(string barcode, int quantity, int userId);
        public Task ReceiveProductAsync(string barcode, int quantity, int userId);
        public Task<bool> IsStockLow(string barcode);
    }
}