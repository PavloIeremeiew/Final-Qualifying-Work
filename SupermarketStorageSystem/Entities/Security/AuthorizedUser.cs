using SupermarketStorageSystem.Entities.Log;

namespace SupermarketStorageSystem.Entities.Security
{
    public class AuthorizedUser
    {
        public string? Id { get; set; }
        public string? Username { get; set; }
        public string? PasswordHash { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }
        public ICollection<InventoryLog>? InventoryLogs { get; set; }
    }
}