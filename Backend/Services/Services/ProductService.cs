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
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ApiResponse<Product>> CreateProductAsync(Product product)
        {
            await _unitOfWork.ProductRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<Product>
            {
                Data = product,
                Succeeded = true,
                Message = "Product created successfully."
            };
        }

        public async Task<ApiResponse<bool>> DeleteProductAsync(int id)
        {
            await _unitOfWork.ProductRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Succeeded = true,
                Message = "Product deleted successfully."
            };
        }

        public async Task<ApiResponse<List<Product>>> GetAllProductsAsync()
        {
            var products = await _unitOfWork.ProductRepository.GetAllAsync();
            return new ApiResponse<List<Product>>
            {
                Data = products,
                Succeeded = true,
                Message = "Products list retrieved successfully."
            };
        }

        public async Task<ApiResponse<Product>> GetProductByIdAsync(int id)
        {
            var product = await _unitOfWork.ProductRepository.GetByIdAsync(id);
            return new ApiResponse<Product>
            {
                Data = product,
                Succeeded = true,
                Message = "Product retrieved successfully."
            };
        }

        public async Task<ApiResponse<List<Product>>> GetProductsByCategoryIdAsync(int categoryId)
        {
            var products = await _unitOfWork.ProductRepository.GetProductsByCategoryIdAsync(categoryId);
            return new ApiResponse<List<Product>>
            {
                Data = products,
                Succeeded = true,
                Message = "Products by category retrieved successfully."
            };
        }

        public async Task<ApiResponse<Product>> UpdateProductAsync(Product product)
        {
            var existingProduct = await _unitOfWork.ProductRepository.GetByIdAsync(product.ProductId);
            if (existingProduct == null)
            {
                return new ApiResponse<Product>
                {
                    Succeeded = false,
                    Message = "Product not found."
                };
            }

            existingProduct.ProductName = product.ProductName;
            existingProduct.Description = product.Description;
            existingProduct.Unit = product.Unit;
            existingProduct.SellingPrice = product.SellingPrice;

            await _unitOfWork.ProductRepository.UpdateAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<Product>
            {
                Data = product,
                Succeeded = true,
                Message = "Product updated successfully."
            };
        }
    }
}
