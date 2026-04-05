namespace SupermarketStorageSystem.Entities.DTOs
{
    public record ProductResponseDto(
        int Id,
        string Name,
        string Barcode,
        int CurrentStock,
        string CategoryName,
        bool IsLowStock
    );
}