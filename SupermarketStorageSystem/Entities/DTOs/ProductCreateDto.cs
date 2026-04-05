namespace SupermarketStorageSystem.Entities.DTOs
{
    public record ProductCreateDto(
        string Name,
        string Barcode,
        decimal PurchasePrice,
        int CurrentStock,
        int MinStockLevel,
        DateTime ExpiryDate,
        int CategoryId
    );
}