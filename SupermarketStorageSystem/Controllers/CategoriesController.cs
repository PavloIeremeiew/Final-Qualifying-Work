using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Entities.Core;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(IApplicationDbContext context) : ControllerBase
    {
        private readonly IApplicationDbContext _context = context;

        [HttpPost]
        public async Task<IActionResult> AddCategory(string name)
        {
            var category = new Category { Name = name };
            await _context.SaveChangesAsync();
            return Ok(category);
        }

        [HttpGet("{id}/products")]
        public async Task<IActionResult> GetProductsByCategory(int id)
        {
            var products = await _context.Products
                .Where(p => p.CategoryId == id)
                .ToListAsync();
            return Ok(products);
        }
    }
}