using Microsoft.AspNetCore.Identity;
using SupermarketStorageSystem.Entities.Security;

namespace SupermarketStorageSystem.Data.Identity
{
    public class ApplicationUserIdentity : IdentityUser
    {
        public string? FullName { get; set; }

        public AuthorizedUser ToAuthorizedUser()
        {
            return new AuthorizedUser
            {
                Id = this.Id,
                Username = this.UserName,
                PasswordHash = this.PasswordHash
            };
        }
    }
}