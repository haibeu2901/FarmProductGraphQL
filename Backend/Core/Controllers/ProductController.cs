using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;
using Services.Services;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts()
        {
            var response = await _productService.GetAllProductsAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetProductById")]
        public async Task<IActionResult> GetProductById([FromQuery] int productId)
        {
            var response = await _productService.GetProductByIdAsync(productId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("CreateProduct")]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            var response = await _productService.CreateProductAsync(product);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([FromBody] Product product)
        {
            var response = await _productService.UpdateProductAsync(product);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([FromQuery] int productId)
        {
            var response = await _productService.DeleteProductAsync(productId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetProductsByCategoryId")]
        public async Task<IActionResult> GetProductsByCategoryId([FromQuery] int categoryId)
        {
            var response = await _productService.GetProductsByCategoryIdAsync(categoryId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
