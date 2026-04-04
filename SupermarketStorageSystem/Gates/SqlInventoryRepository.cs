using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Log;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Entities.Constant;
using SupermarketStorageSystem.Applications;

namespace SupermarketStorageSystem.Gates
{
    public class SqlInventoryRepository(IApplicationDbContext context) : IInventoryRepository
    {
        private readonly IApplicationDbContext _context = context;

        public async Task<Product> GetByBarcodeAsync(string barcode)
        {
            return await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Barcode == barcode)
                    ?? throw new Exception(ErrorsMessages.ProductNotFound);
        }

        public async Task<Product> GetByIdAsync(int id)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == id) ?? throw new Exception(ErrorsMessages.ProductNotFound);
            return product;
        }

        public async Task UpdateProductAsync(Product product)
        {
            _context.UpdateProduct(product);
            await Task.CompletedTask;
        }

        public async Task AddLogAsync(InventoryLog log)
        {
            await _context.AddLogAsync(log);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}