using Microsoft.AspNetCore.Mvc;
using SupermarketStorageSystem.Applications.Services;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController(IInventoryService inventoryService) : ControllerBase
    {
        private readonly IInventoryService _inventoryService = inventoryService;

        [HttpPost("inventory-reconciliation")]
        public async Task<IActionResult> Reconcile(string barcode, int actualQuantity, int userId)
        {
            await _inventoryService.ProcessInventory(barcode, actualQuantity, userId);
            return Ok(new { Message = "Інвентаризація успішно проведена" });
        }

        [HttpPost("sell")]
        public async Task<IActionResult> Sell(string barcode, int quantity, int userId)
        {
            try 
            {
                await _inventoryService.SellProductAsync(barcode, quantity, userId);
                return Ok(new { Message = "Продаж зафіксовано" });
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