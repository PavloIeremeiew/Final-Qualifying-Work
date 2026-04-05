using SupermarketStorageSystem.Entities.Constant;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Entities.DTOs;

namespace SupermarketStorageSystem.Applications.Services
{
    public class InventoryService(IInventoryRepository repository, IMappingService mappingService) : IInventoryService
    {
        private readonly IInventoryRepository _repository = repository;
        private readonly IMappingService _mappingService = mappingService;

        public async Task<LogDTO> ProcessInventory(string barcode, int actualQuantity, string userId)
        {
            var product = await GetProductByBarcodeAsync(barcode);

            int difference = actualQuantity - product.CurrentStock;
            product.CurrentStock = actualQuantity;

            var log = CreateInventoryLog(userId, product, difference, OperationType.InventoryAdjustment);
            await SaveProductAndLogAsync(product, log);
            return _mappingService.MapToLogDTO(log, product.Name);
        }

        public async Task<LogDTO> SellProductAsync(string barcode, int quantity, string userId)
        {
            var product = await GetProductByBarcodeAsync(barcode);

            if (product.CurrentStock < quantity)
                throw new Exception(StockValidationMessages.InsufficientStock);

            product.CurrentStock -= quantity;

            var log = CreateInventoryLog(userId, product, -quantity, OperationType.Sale);
            await SaveProductAndLogAsync(product, log);
            return _mappingService.MapToLogDTO(log, product.Name);
        }

        public async Task<LogDTO> ReceiveProductAsync(string barcode, int quantity, string userId)
        {
            var product = await GetProductByBarcodeAsync(barcode);

            product.CurrentStock += quantity;

            var log = CreateInventoryLog(userId, product, quantity, OperationType.Receive);
            await SaveProductAndLogAsync(product, log);
            return _mappingService.MapToLogDTO(log, product.Name);
        }

        public async Task<bool> IsStockLow(string barcode)
        {
            var product = await _repository.GetByBarcodeAsync(barcode);
            return product.CurrentStock <= product.MinStockLevel;
        }

        public async Task<IEnumerable<ProductResponseDto>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(_mappingService.MapToProductResponseDto);
        }

        private async Task<Product> GetProductByBarcodeAsync(string barcode)
        {
            return await _repository.GetByBarcodeAsync(barcode)
                                ?? throw new Exception(StockValidationMessages.ProductNotFound);
        }

        private static InventoryLog CreateInventoryLog(string userId, Product product, int change, OperationType operationType)
        {
            return new InventoryLog
            {
                ProductId = product.Id,
                QuantityChange = change,
                ResultingStock = product.CurrentStock,
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