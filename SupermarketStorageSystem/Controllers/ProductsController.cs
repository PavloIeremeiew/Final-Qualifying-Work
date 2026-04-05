using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SupermarketStorageSystem.Applications;
using SupermarketStorageSystem.Entities.Core;
using SupermarketStorageSystem.Entities.Constant;
using SupermarketStorageSystem.Entities.Log;
using SupermarketStorageSystem.Entities.DTOs;
using SupermarketStorageSystem.Applications.Services;

namespace SupermarketStorageSystem.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController(IApplicationDbContext context, IInventoryService inventoryService, IMappingService mappingService) : ControllerBase
    {
        private readonly IApplicationDbContext _context = context;
        private readonly IInventoryService _inventoryService = inventoryService;
        private readonly IMappingService _mappingService = mappingService;

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _inventoryService.GetAllProductsAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto dto, string? userId)
        {
            try
            {
                Product product = _mappingService.MapToProduct(dto);

                await _context.AddProductAsync(product);
                await _context.SaveChangesAsync();

                var log = CreateInventoryLog(product, userId);
                await _context.AddLogAsync(log);
                await _context.SaveChangesAsync();

                return Ok(new { Message = StockValidationMessages.SucsessfulOperation, ProductId = product.Id });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        private static InventoryLog CreateInventoryLog(Product product, string? userId)
        {
            return new InventoryLog
            {
                ProductId = product.Id,
                OperationType = OperationType.AddNewProduct.ToString(),
                Timestamp = DateTime.Now,
                UserId = userId,
                QuantityChange = product.CurrentStock
            };
        }
    }
}