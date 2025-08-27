using BusinessObject.Data;
using BusinessObject.Models;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.Pagination;
using Core.GraphQL.Inputs;
using HotChocolate;
using Microsoft.EntityFrameworkCore;
using Services.Interfaces;

namespace Core.GraphQL.Queries
{
    [ExtendObjectType("Query")]
    public class OrderQueries
    {
        public async Task<List<OrderResponseWithDetails>> GetOrders([Service] IOrderService orderService)
        {
            var response = await orderService.GetAllOrdersAsync();
            return response.Succeeded ? response.Data : new List<OrderResponseWithDetails>();
        }

        public async Task<PaginatedResult<OrderResponseWithDetails>> GetOrdersPaginated(
            PaginationInput pagination,
            [Service] IOrderService orderService)
        {
            var request = new PaginationRequest
            {
                PageNumber = pagination.PageNumber,
                PageSize = pagination.PageSize
            };

            return await orderService.GetOrdersPaginatedGraphQLAsync(request);
        }

        public async Task<OrderResponseWithDetails?> GetOrderById(int id, [Service] IOrderService orderService)
        {
            var response = await orderService.GetOrderByIdAsync(id);
            return response.Succeeded ? response.Data : null;
        }

        public async Task<OrderResponseWithDetails?> GetLatestOrder([Service] IOrderService orderService)
        {
            var response = await orderService.GetLatestOrderAsync();
            return response.Succeeded ? response.Data : null;
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Order> GetOrders([Service] FarmProductsApiContext context)
        {
            return context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductCategory);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Order?> GetOrderById(int id, [Service] FarmProductsApiContext context)
        {
            return await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductCategory)
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public async Task<Order?> GetLatestOrder([Service] FarmProductsApiContext context)
        {
            return await context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductCategory)
                .OrderByDescending(o => o.OrderDate)
                .FirstOrDefaultAsync();
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Order> GetOrdersByCustomer(int customerId, [Service] FarmProductsApiContext context)
        {
            return context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductCategory)
                .Where(o => o.CustomerId == customerId);
        }

        [UseProjection]
        [UseFiltering]
        [UseSorting]
        public IQueryable<Order> GetOrdersByDateRange(DateTime startDate, DateTime endDate, [Service] FarmProductsApiContext context)
        {
            return context.Orders
                .Include(o => o.Customer)
                .Include(o => o.Staff)
                .Include(o => o.OrderDetails)
                    .ThenInclude(od => od.Product)
                        .ThenInclude(p => p.ProductCategory)
                .Where(o => o.OrderDate >= startDate && o.OrderDate <= endDate);
        }
    }
}