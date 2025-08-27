using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImportedStockService : ControllerBase
    {
        private readonly IImportedStockService _importedStockService;

        public ImportedStockService(IImportedStockService importedStockService)
        {
            _importedStockService = importedStockService;
        }

        [HttpGet("GetAllImportedStock")]
        public async Task<IActionResult> GetAllImportedStock()
        {
            var response = await _importedStockService.GetAllImportedStocksAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetImportedStockById")]
        public async Task<IActionResult> GetImportedStockById([FromQuery] int importedId)
        {
            var response = await _importedStockService.GetImportedStockByIdAsync(importedId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("CreateImportedStock")]
        public async Task<IActionResult> CreateImportedStock([FromBody] ImportedStock importedStock)
        {
            if (importedStock == null)
            {
                return BadRequest("Imported stock can not be null");
            }

            var response = await _importedStockService.CreateImportedStockAsync(importedStock);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateImportedStock")]
        public async Task<IActionResult> UpdateImportedStock([FromBody] ImportedStock importedStock)
        {
            if (importedStock == null)
            {
                return BadRequest("Imported stock can not be null");
            }

            var response = await _importedStockService.UpdateImportedStockAsync(importedStock);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteImportedStock")]
        public async Task<IActionResult> DeleteImportedStock([FromQuery] int importedId)
        {
            var response = await _importedStockService.DeleteImportedStockAsync(importedId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
