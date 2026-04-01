using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Data.Context;
using SupermarketStorageSystem.Core.Constant;
using Microsoft.EntityFrameworkCore;

namespace SupermarketStorageSystem.Core.Repository
{
    public class SqlInventoryRepository(InventoryDbContext context) : IInventoryRepository
    {
        private readonly InventoryDbContext _context = context;

        public async Task<Product> GetByBarcodeAsync(string barcode)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Barcode == barcode)
                    ?? throw new Exception(ErrorsMessages.ProductNotFound);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.FindAsync(id) ?? throw new Exception(ErrorsMessages.ProductNotFound);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.Products.Update(product);
            await Task.CompletedTask;
        }

        public async Task AddLogAsync(InventoryLog log)
        {
            await _context.InventoryLogs.AddAsync(log);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}