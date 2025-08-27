using BusinessObject.Data;
using BusinessObject.Models;
using HotChocolate;
using Microsoft.EntityFrameworkCore;

namespace Core.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class CategoryQueries
    {
        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<ProductCategory> GetAllCategories([Service] FarmProductsApiContext context)
        {
            return context.ProductCategories
                .Include(c => c.Products)
                    .ThenInclude(p => p.OrderDetails)
                        .ThenInclude(od => od.Order);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<ProductCategory?> GetCategoryById(int id, [Service] FarmProductsApiContext context)
        {
            return await context.ProductCategories
                .Include(c => c.Products)
                    .ThenInclude(p => p.OrderDetails)
                        .ThenInclude(od => od.Order)
                .FirstOrDefaultAsync(c => c.CategoryId == id);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<ProductCategory?> GetCategoryByName(string name, [Service] FarmProductsApiContext context)
        {
            return await context.ProductCategories
                .Include(c => c.Products)
                    .ThenInclude(p => p.OrderDetails)
                        .ThenInclude(od => od.Order)
                .FirstOrDefaultAsync(c => c.CategoryName == name);
        }
    }
}