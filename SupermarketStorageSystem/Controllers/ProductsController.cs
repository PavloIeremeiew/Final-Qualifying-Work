using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Entities.Core;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IApplicationDbContext context) : ControllerBase
    {
        private readonly IApplicationDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products.Include(p => p.Category).ToListAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product)
        {
            if (await _context.Products.AnyAsync(p => p.Barcode == product.Barcode))
                return BadRequest("Товар з таким штрих-кодом вже існує");

            await _context.AddLogAsync(new Entities.Log.InventoryLog
            {
                ProductId = product.Id,
                OperationType = "Створення",
                Timestamp = DateTime.Now,
                QuantityChange = product.CurrentStock
            });

            return Ok(new { Message = "Товар додано" });
        }
    }
}