using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LogsController(IApplicationDbContext context) : ControllerBase
    {
        private readonly IApplicationDbContext _context = context;

        [HttpGet]
        public async Task<IActionResult> GetHistory(int? productId)
        {
            var query = _context.InventoryLogs.AsQueryable();
            
            if (productId.HasValue)
                query = query.Where(l => l.ProductId == productId.Value);

            var logs = await query
                .OrderByDescending(l => l.Timestamp)
                .Take(100)
                .ToListAsync();

            return Ok(logs);
        }
    }
}