using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Applications.Services;
using SupermarketStorageSystem.Entities.Core;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController(IApplicationDbContext context, IMappingService mappingService) : ControllerBase
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IMappingService _mappingService = mappingService;

        [HttpGet]
        public async Task<IActionResult> GetHistory(int? productId)
        {
            var query = _context.InventoryLogs.AsQueryable();
            Product? product = null;

            if (productId.HasValue)
            {
                query = query.Where(l => l.ProductId == productId.Value);
                product = await _context.Products.FirstOrDefaultAsync(p => p.Id == productId.Value);
            }

            var logs = await query
                .OrderByDescending(l => l.Timestamp)
                .Take(100)
                .ToListAsync();

            return Ok(logs.Select(log => _mappingService.MapToLogDTO(log, product?.Name)));
        }
    }
}