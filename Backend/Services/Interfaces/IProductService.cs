using BusinessObject.Models;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IProductService
    {
        Task<ApiResponse<List<Product>>> GetAllProductsAsync();
        Task<ApiResponse<Product>> GetProductByIdAsync(int id);
        Task<ApiResponse<Product>> CreateProductAsync(Product product);
        Task<ApiResponse<Product>> UpdateProductAsync(Product product);
        Task<ApiResponse<bool>> DeleteProductAsync(int id);
        Task<ApiResponse<List<Product>>> GetProductsByCategoryIdAsync(int categoryId);
    }
}
