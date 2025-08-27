using BusinessObject.Models;
using Repository;
using Services.Interfaces;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<ProductCategory>> CreateCategoryAsync(ProductCategory category)
        {
            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<ProductCategory>
            {
                Data = category,
                Succeeded = true,
                Message = "Category created successfully."
            };
        }

        public async Task<ApiResponse<bool>> DeleteCategoryAsync(int id)
        {
            await _unitOfWork.CategoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Succeeded = true,
                Message = "Category deleted successfully."
            };
        }

        public async Task<ApiResponse<List<ProductCategory>>> GetAllCategoriesAsync()
        {
            var categories = await _unitOfWork.CategoryRepository.GetAllAsync();
            return new ApiResponse<List<ProductCategory>>
            {
                Data = categories,
                Succeeded = true,
                Message = "Categories list retrieved successfully."
            };
        }

        public async Task<ApiResponse<ProductCategory>> GetCategoryByIdAsync(int id)
        {
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return new ApiResponse<ProductCategory>
                {
                    Succeeded = false,
                    Message = "Category not found."
                };
            }
            return new ApiResponse<ProductCategory>
            {
                Data = category,
                Succeeded = true,
                Message = "Category retrieved successfully."
            };
        }

        public async Task<ApiResponse<ProductCategory>> UpdateCategoryAsync(ProductCategory category)
        {
            var existingCategory =await _unitOfWork.CategoryRepository.GetByIdAsync(category.CategoryId);
            if (existingCategory == null)
            {
                return new ApiResponse<ProductCategory>
                {
                    Succeeded = false,
                    Message = "Category not found."
                };
            }
            existingCategory.CategoryName = category.CategoryName;
            existingCategory.Description = category.Description;

            await _unitOfWork.CategoryRepository.UpdateAsync(existingCategory);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<ProductCategory>
            {
                Data = existingCategory,
                Succeeded = true,
                Message = "Category updated successfully."
            };
        }
    }
}
