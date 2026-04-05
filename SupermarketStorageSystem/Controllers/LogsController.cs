using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Applications.Services;

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

            if (productId.HasValue)
                query = query.Where(l => l.ProductId == productId.Value);

            var logs = await query
                .OrderByDescending(l => l.Timestamp)
                .Take(100)
                .ToListAsync();

            return Ok(logs.Select(log => _mappingService.MapToLogDTO(log, log.Product?.Name)));
        }
    }
}