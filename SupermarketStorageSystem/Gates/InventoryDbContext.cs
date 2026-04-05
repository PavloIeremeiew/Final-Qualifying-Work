using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Entities.Constant;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Entities.Security;

namespace SupermarketStorageSystem.Gates
{
    public class InventoryDbContext(DbContextOptions<InventoryDbContext> options) : DbContext(options), IApplicationDbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<InventoryLog> InventoryLogs { get; set; }
        public DbSet<AuthorizedUser> AuthorizedUsers { get; set; }
        public DbSet<Role> Roles { get; set; }

        IQueryable<Product> IApplicationDbContext.Products => Products;
        IQueryable<Category> IApplicationDbContext.Categories => Categories;
        IQueryable<InventoryLog> IApplicationDbContext.InventoryLogs => InventoryLogs;

        public void UpdateProduct(Product product)
        {
            Products.Update(product);
        }

        public async Task AddLogAsync(InventoryLog log, CancellationToken cancellationToken = default)
        {
            await InventoryLogs.AddAsync(log, cancellationToken);
        }
        public async Task AddProductAsync(Product product)
        {
            await Products.AddAsync(product);
        }

        public async Task AddCategoryAsync(Category category)
        {
            await Categories.AddAsync(category);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Product>().Property(p => p.PurchasePrice).HasPrecision(18, 2);
            modelBuilder.Entity<Product>().HasIndex(p => p.Barcode).IsUnique();
            modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired().HasMaxLength(200);

            AddMockValue(modelBuilder);
        }

        private static void AddMockValue(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Role>().HasData(MockRoles());
            modelBuilder.Entity<AuthorizedUser>().HasData(MockUsers());
        }

        private static List<Role> MockRoles()
        {
            List<Role> roles = [];
            foreach (var role in Enum.GetValues<RoleType>())
            {
                roles.Add(new Role { Id = (int)role + 1, Name = role.ToString() });
            }

            return roles;
        }

        private static List<AuthorizedUser> MockUsers()
        {
            List<AuthorizedUser> users = [];
            users.Add(new AuthorizedUser
            {
                Id = "adminID",
                Username = "admin",
                PasswordHash = "PasswordHashadmin123",
                RoleId = (int)RoleType.Administrator + 1
            });

            users.Add(new AuthorizedUser
            {
                Id = "managerID",
                Username = "manager",
                PasswordHash = "PasswordHashmanager123",
                RoleId = (int)RoleType.Manager + 1
            });

            users.Add(new AuthorizedUser
            {
                Id = "storekeeperID",
                Username = "storekeeper",
                PasswordHash = "PasswordHashstorekeeper123",
                RoleId = (int)RoleType.Storekeeper + 1
            });

            return users;
        }
    }
}