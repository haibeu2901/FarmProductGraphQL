using BusinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories()
        {
            var response = await _categoryService.GetAllCategoriesAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetCategoryById")]
        public async Task<IActionResult> GetCategoryById([FromQuery] int id)
        {
            var response = await _categoryService.GetCategoryByIdAsync(id);
            if (!response.Succeeded)
            {
                return NotFound(response);
            }
            return Ok(response);
        }

        [HttpPost("CreateCategory")]
        public async Task<IActionResult> CreateCategory([FromBody] ProductCategory category)
        {
            if (category == null)
            {
                return BadRequest("Category cannot be null.");
            }
            var response = await _categoryService.CreateCategoryAsync(category);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return CreatedAtAction(nameof(GetCategoryById), new { id = response.Data.CategoryId }, response);
        }

        [HttpPut("UpdateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] ProductCategory category)
        {
            if (category == null || category.CategoryId <= 0)
            {
                return BadRequest("Invalid category data.");
            }
            var response = await _categoryService.UpdateCategoryAsync(category);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromQuery] int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid category ID.");
            }
            var response = await _categoryService.DeleteCategoryAsync(id);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
