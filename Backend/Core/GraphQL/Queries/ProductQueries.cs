using BusinessObject.Data;
using BusinessObject.Models;
using BusinessObject.ViewModels.Pagination;
using Core.GraphQL.Inputs;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Core.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class ProductQueries
    {
        public async Task<List<Product>> GetProducts([Service] IProductService productService)
        {
            var response = await productService.GetAllProductsAsync();
            return response.Succeeded ? response.Data : new List<Product>();
        }

        public async Task<Product?> GetProductById(int id, [Service] IProductService productService)
        {
            var response = await productService.GetProductByIdAsync(id);
            return response.Succeeded ? response.Data : null;
        }
        public async Task<List<Product>> GetProductsByCategoryId(int categoryId, [Service] IProductService productService)
        {
            var response = await productService.GetProductsByCategoryIdAsync(categoryId);
            return response.Succeeded ? response.Data : new List<Product>();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProducts([Service] FarmProductsApiContext context)
        {
            return context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.OrderDetails)
                    .ThenInclude(od => od.Order)
                .Include(p => p.ImportedStocks);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Product?> GetProductById(int id, [Service] FarmProductsApiContext context)
        {
            return await context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.OrderDetails)
                    .ThenInclude(od => od.Order)
                .Include(p => p.ImportedStocks)
                .FirstOrDefaultAsync(p => p.ProductId == id);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProductsByCategory(int categoryId, [Service] FarmProductsApiContext context)
        {
            return context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.OrderDetails)
                    .ThenInclude(od => od.Order)
                .Include(p => p.ImportedStocks)
                .Where(p => p.CategoryId == categoryId);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProductsInStock([Service] FarmProductsApiContext context)
        {
            return context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.OrderDetails)
                    .ThenInclude(od => od.Order)
                .Include(p => p.ImportedStocks)
                .Where(p => p.Quantity > 0);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Product> GetProductsByPriceRange(decimal minPrice, decimal maxPrice, [Service] FarmProductsApiContext context)
        {
            return context.Products
                .Include(p => p.ProductCategory)
                .Include(p => p.OrderDetails)
                    .ThenInclude(od => od.Order)
                .Include(p => p.ImportedStocks)
                .Where(p => p.SellingPrice >= minPrice && p.SellingPrice <= maxPrice);
        }
    }
}
