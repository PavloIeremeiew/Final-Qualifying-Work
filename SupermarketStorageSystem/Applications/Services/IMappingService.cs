using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.DTOs;
using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Applications.Services
{
    public interface IMappingService
    {
        public ProductResponseDto MapToProductResponseDto(Product product);
        public Product MapToProduct(ProductCreateDto productDto);
        public LogDTO MapToLogDTO(InventoryLog log, string? productName);
    }
}