using BusinessObject.Models;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface ICategoryService
    {
        Task<ApiResponse<List<ProductCategory>>> GetAllCategoriesAsync();
        Task<ApiResponse<ProductCategory>> GetCategoryByIdAsync(int id);
        Task<ApiResponse<ProductCategory>> CreateCategoryAsync(ProductCategory category);
        Task<ApiResponse<ProductCategory>> UpdateCategoryAsync(ProductCategory category);
        Task<ApiResponse<bool>> DeleteCategoryAsync(int id);
    }
}
