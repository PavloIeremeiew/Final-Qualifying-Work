using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Security;

namespace SupermarketStorageSystem.Entities.Log
{
    public class InventoryLog
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int QuantityChange { get; set; }
        public string? OperationType { get; set; }
        public DateTime Timestamp { get; set; }
        public int UserId { get; set; }
        public Product? Product { get; set; }
        public AuthorizedUser? User { get; set; }
    }
}