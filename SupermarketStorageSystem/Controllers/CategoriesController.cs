using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Applications.Services;
using SupermarketStorageSystem.Entities.Core;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController(IApplicationDbContext context, IMappingService mappingService) : ControllerBase
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMappingService _mappingService = mappingService;

        [HttpPost]
        public async Task<IActionResult> AddCategory(string name)
        {
            try
            {
                var category = new Category { Name = name };
                await _context.AddCategoryAsync(category);
                await _context.SaveChangesAsync();
                return Ok(category);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("products")]
        public async Task<IActionResult> GetProductsByCategoryId(int? id)
        {
            try
            {
                var products = await _context.Products
                    .Where(p => p.CategoryId == id)
                    .ToListAsync();
                var dtos = products.Select(_mappingService.MapToProductResponseDto);
                return Ok(dtos);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return Ok(categories);
        }
    }
}