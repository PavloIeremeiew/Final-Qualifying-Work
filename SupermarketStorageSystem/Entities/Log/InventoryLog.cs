using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Security;

namespace SupermarketStorageSystem.Entities.Log
{
    public class InventoryLog
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; } // Навігаційна властивість
        public int QuantityChange { get; set; }
        public string? QuantityType { get; set; } // "Додано", "Видалено", "Оновлено"
        public DateTime Timestamp { get; set; }
        public AuthorizedUser? AuthorizedUser { get; set; }
        public int AuthorizedUserId { get; set; }
    }
}