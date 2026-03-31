using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Core.Repository;
using SupermarketStorageSystem.Entities.Core;

namespace SupermarketStorageSystem.Core.Services
{
    public class InventoryService
    {
        private readonly IInventoryRepository _repository;

        public InventoryService(IInventoryRepository repository) => _repository = repository;

        public async Task ProcessInventory(string barcode, int actualQuantity, int userId)
        {
            InventoryLog? log = null!;
            var product =
                await _repository.GetByBarcodeAsync(barcode)
                    ?? throw new Exception("Товар не знайдено");

            int difference = actualQuantity - product.CurrentStock;

            if (difference != 0)
            {
                log = new InventoryLog
                {
                    ProductId = product.Id,
                    QuantityChange = difference,
                    QuantityType = "InventoryAdjustment",
                    Timestamp = DateTime.Now,
                    AuthorizedUserId = userId
                };
            }

            product.CurrentStock = actualQuantity;
            await SaveProductAndLogAsync(product, log);
        }

        private async Task SaveProductAndLogAsync(Product product, InventoryLog log)
        {
            await _repository.UpdateProductAsync(product);
            if (log != null)
                await _repository.AddLogAsync(log);
            await _repository.SaveChangesAsync();
        }

        public async Task SellProductAsync(string barcode, int quantity, int userId)
        {
            var product =
                await _repository.GetByBarcodeAsync(barcode)
                    ?? throw new Exception("Товар не знайдено.");

            if (product.CurrentStock < quantity)
                throw new Exception("Недостатньо товару на складі.");

            product.CurrentStock -= quantity;

            var log = new InventoryLog
            {
                ProductId = product.Id,
                QuantityChange = -quantity,
                QuantityType = "Sale",
                Timestamp = DateTime.Now,
                AuthorizedUserId = userId
            };

            await SaveProductAndLogAsync(product, log);
        }

        public async Task ReceiveProductAsync(string barcode, int quantity, int userId)
        {
            var product =
                await _repository.GetByBarcodeAsync(barcode)
                    ?? throw new Exception("Товар не зареєстровано в каталозі.");

            product.CurrentStock += quantity;

            var log = new InventoryLog
            {
                ProductId = product.Id,
                QuantityChange = quantity,
                QuantityType = "Receive",
                Timestamp = DateTime.Now,
                AuthorizedUserId = userId
            };

            await SaveProductAndLogAsync(product, log);
        }

        public async Task<bool> IsStockLow(string barcode)
        {
            var product = await _repository.GetByBarcodeAsync(barcode);
            return product.CurrentStock <= product.MinStockLevel;
        }
    }
}