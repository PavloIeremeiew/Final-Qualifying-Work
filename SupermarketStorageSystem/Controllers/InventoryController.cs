using Microsoft.AspNetCore.Mvc;
using SupermarketStorageSystem.Applications.Services;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Entities.Constant;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController(IInventoryService inventoryService, IApplicationDbContext context) : ControllerBase
    {
        private readonly IInventoryService _inventoryService = inventoryService;
        private readonly IApplicationDbContext _context = context;

        [HttpPost("inventory-reconciliation")]
        public async Task<IActionResult> Reconcile(string barcode, int actualQuantity, string userId)
        {
            try
            {
                var log = await _inventoryService.ProcessInventory(barcode, actualQuantity, userId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = StockValidationMessages.SucsessfulOperation, Log = log });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell(string barcode, int quantity, string userId)
        {
            try
            {
                var log = await _inventoryService.SellProductAsync(barcode, quantity, userId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = StockValidationMessages.SucsessfulOperation, Log = log });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("receive")]
        public async Task<IActionResult> Receive(string barcode, int quantity, string userId)
        {
            try
            {
                var log = await _inventoryService.ReceiveProductAsync(barcode, quantity, userId);
                await _context.SaveChangesAsync();
                return Ok(new { Message = StockValidationMessages.SucsessfulOperation, Log = log });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("stock-status/{barcode}")]
        public async Task<IActionResult> GetStockStatus(string barcode)
        {
            var isLow = await _inventoryService.IsStockLow(barcode);
            return Ok(new { Barcode = barcode, NeedsRestock = isLow });
        }
    }
}