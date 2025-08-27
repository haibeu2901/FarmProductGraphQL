using BusinessObject.Models;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.Pagination;
using Services.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IOrderService
    {
        Task<ApiResponse<List<OrderResponseWithDetails>>> GetAllOrdersAsync();
        Task<ApiResponse<OrderResponseWithDetails>> GetOrderByIdAsync(int id);
        Task<ApiResponse<OrderResponseWithDetails>> CreateOrderAsync(Order order, List<OrderDetail> orderDetails);
        Task<ApiResponse<OrderResponseWithDetails>> UpdateOrderAsync(Order order, List<OrderDetail> orderDetails);
        Task<ApiResponse<bool>> DeleteOrderAsync(int id);
        Task<ApiResponse<OrderResponseWithDetails>> GetLatestOrderAsync();
        Task<PaginatedApiResponse<OrderResponseWithDetails>> GetOrdersPaginatedAsync(PaginationRequest request);
        Task<PaginatedResult<OrderResponseWithDetails>> GetOrdersPaginatedGraphQLAsync(PaginationRequest request);
    }
}
