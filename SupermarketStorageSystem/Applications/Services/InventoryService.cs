using SupermarketStorageSystem.Entities.Constant;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Applications.Services
{
    public class InventoryService(IInventoryRepository repository) : IInventoryService
    {
        private readonly IInventoryRepository _repository = repository;

        public async Task ProcessInventory(string barcode, int actualQuantity, int userId)
        {
            var product = await GetProductByBarcodeAsync(barcode);

            int difference = actualQuantity - product.CurrentStock;
            product.CurrentStock = actualQuantity;

            var log = difference != 0 ? CreateInventoryLog(userId, product, difference, OperationType.InventoryAdjustment) : null!;
            await SaveProductAndLogAsync(product, log);
        }

        public async Task SellProductAsync(string barcode, int quantity, int userId)
        {
            var product = await GetProductByBarcodeAsync(barcode);

            if (product.CurrentStock < quantity)
                throw new Exception(ErrorsMessages.InsufficientStock);

            product.CurrentStock -= quantity;

            var log = CreateInventoryLog(userId, product, -quantity, OperationType.Sale);
            await SaveProductAndLogAsync(product, log);
        }

        public async Task ReceiveProductAsync(string barcode, int quantity, int userId)
        {
            var product = await GetProductByBarcodeAsync(barcode);

            product.CurrentStock += quantity;

            var log = CreateInventoryLog(userId, product, quantity, OperationType.Receive);
            await SaveProductAndLogAsync(product, log);
        }

        public async Task<bool> IsStockLow(string barcode)
        {
            var product = await _repository.GetByBarcodeAsync(barcode);
            return product.CurrentStock <= product.MinStockLevel;
        }

        private async Task<Product> GetProductByBarcodeAsync(string barcode)
        {
            return await _repository.GetByBarcodeAsync(barcode)
                                ?? throw new Exception(ErrorsMessages.ProductNotFound);
        }

        private static InventoryLog CreateInventoryLog(int userId, Product product, int change, OperationType operationType)
        {
            return new InventoryLog
            {
                ProductId = product.Id,
                QuantityChange = change,
                OperationType = operationType.ToString(),
                Timestamp = DateTime.Now,
                UserId = userId
            };
        }

        private async Task SaveProductAndLogAsync(Product product, InventoryLog log)
        {
            await _repository.UpdateProductAsync(product);
            if (log != null)
                await _repository.AddLogAsync(log);
            await _repository.SaveChangesAsync();
        }
    }
}