namespace SupermarketStorageSystem.Entities.DTOs
{
    public record LogDTO(
        int LogId,
        string ProductName,
        int QuantityChange,
        int ResultingStock,
        string OperationType,
        DateTime Timestamp,
        string UserName
    );
}