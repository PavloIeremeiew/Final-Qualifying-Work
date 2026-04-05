using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.DTOs;
using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Applications.Services;

namespace SupermarketStorageSystem.Gates
{
    public class MappingService : IMappingService
    {
        public ProductResponseDto MapToProductResponseDto(Product product)
        {
            return new ProductResponseDto(
                product.Id,
                product.Name ?? string.Empty,
                product.Barcode ?? string.Empty,
                product.CurrentStock,
                product.Category?.Name ?? string.Empty,
                product.CurrentStock <= product.MinStockLevel
            );
        }

        public Product MapToProduct(ProductCreateDto productDto)
        {
            return new Product
            {
                Name = productDto.Name,
                Barcode = productDto.Barcode,
                PurchasePrice = productDto.PurchasePrice,
                CurrentStock = productDto.CurrentStock,
                MinStockLevel = productDto.MinStockLevel,
                ExpiryDate = productDto.ExpiryDate,
                CategoryId = productDto.CategoryId
            };
        }

        public LogDTO MapToLogDTO(InventoryLog log, string? productName)
        {
            return new LogDTO(
                log.Id,
                productName ?? string.Empty,
                log.QuantityChange,
                log.ResultingStock,
                log.OperationType ?? string.Empty,
                log.Timestamp,
                log.UserId ?? string.Empty
            );
        }
    }
}