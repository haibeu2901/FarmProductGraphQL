using AutoMapper;
using BusinessObject.Models;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.Pagination;
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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ApiResponse<OrderResponseWithDetails>> CreateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            foreach (var detail in orderDetails)
            {
                detail.OrderId = order.OrderId;
                await _unitOfWork.OrderDetailRepository.AddAsync(detail);
            }
            await _unitOfWork.OrderRepository.AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            // AutoMapper will automatically map matching property names
            var orderResponse = _mapper.Map<OrderResponseWithDetails>(order);
            return new ApiResponse<OrderResponseWithDetails>
            {
                Data = orderResponse,
                Succeeded = true,
                Message = "Order created successfully."
            };
        }

        public async Task<ApiResponse<bool>> DeleteOrderAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return new ApiResponse<bool>
                {
                    Succeeded = false,
                    Message = "Order not found."
                };
            }

            var orderDetails = await _unitOfWork.OrderDetailRepository.GetOrderDetailsByOrderId(id);
            foreach (var item in orderDetails)
            {
                await _unitOfWork.OrderDetailRepository.DeleteAsync(item.DetailId);
            }
            await _unitOfWork.OrderRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
            return new ApiResponse<bool>
            {
                Data = true,
                Succeeded = true,
                Message = "Order deleted successfully."
            };
        }

        public async Task<ApiResponse<List<OrderResponseWithDetails>>> GetAllOrdersAsync()
        {
            var orders = await _unitOfWork.OrderRepository.GetAllAsync();
            var orderResponses = _mapper.Map<List<OrderResponseWithDetails>>(orders);
            return new ApiResponse<List<OrderResponseWithDetails>>
            {
                Data = orderResponses,
                Succeeded = true,
                Message = "Orders list retrieved successfully."
            };
        }

        public async Task<ApiResponse<OrderResponseWithDetails>> GetOrderByIdAsync(int id)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdAsync(id);
            if (order == null)
            {
                return new ApiResponse<OrderResponseWithDetails>
                {
                    Succeeded = false,
                    Message = "Order not found."
                };
            }
            var orderResponse = _mapper.Map<OrderResponseWithDetails>(order);
            return new ApiResponse<OrderResponseWithDetails>
            {
                Data = orderResponse,
                Succeeded = true,
                Message = "Order retrieved successfully."
            };
        }

        public async Task<ApiResponse<OrderResponseWithDetails>> UpdateOrderAsync(Order order, List<OrderDetail> orderDetails)
        {
            var existingOrder = await _unitOfWork.OrderRepository.GetByIdAsync(order.OrderId);
            if (existingOrder == null)
            {
                return new ApiResponse<OrderResponseWithDetails>
                {
                    Succeeded = false,
                    Message = "Order not found."
                };
            }
            existingOrder.OrderDate = order.OrderDate;
            var existingDetails = await _unitOfWork.OrderDetailRepository.GetOrderDetailsByOrderId(order.OrderId);
            foreach (var detail in existingDetails)
            {
                await _unitOfWork.OrderDetailRepository.UpdateAsync(detail);
            }
            await _unitOfWork.OrderRepository.UpdateAsync(existingOrder);
            await _unitOfWork.SaveChangesAsync();

            var orderResponse = _mapper.Map<OrderResponseWithDetails>(existingOrder);
            return new ApiResponse<OrderResponseWithDetails>
            {
                Data = orderResponse,
                Succeeded = true,
                Message = "Order updated successfully."
            };
        }

        public async Task<ApiResponse<OrderResponseWithDetails>> GetLatestOrderAsync()
        {
            var order = await _unitOfWork.OrderRepository.GetLatestOrderAsync();
            if (order == null)
            {
                return new ApiResponse<OrderResponseWithDetails>
                {
                    Succeeded = false,
                    Message = "No orders found."
                };
            }
            var orderResponse = _mapper.Map<OrderResponseWithDetails>(order);
            return new ApiResponse<OrderResponseWithDetails>
            {
                Data = orderResponse,
                Succeeded = true,
                Message = "Latest order retrieved successfully."
            };
        }

        public async Task<PaginatedApiResponse<OrderResponseWithDetails>> GetOrdersPaginatedAsync(PaginationRequest request)
        {
            var paginatedResult = await _unitOfWork.OrderRepository.GetPaginatedAsync(request);
            var mappedData = _mapper.Map<List<OrderResponseWithDetails>>(paginatedResult.Data);
            
            return new PaginatedApiResponse<OrderResponseWithDetails>(
                mappedData, 
                paginatedResult.TotalCount, 
                paginatedResult.PageNumber, 
                paginatedResult.PageSize);
        }

        public async Task<PaginatedResult<OrderResponseWithDetails>> GetOrdersPaginatedGraphQLAsync(PaginationRequest request)
        {
            var paginatedResult = await _unitOfWork.OrderRepository.GetPaginatedAsync(request);
            var mappedData = _mapper.Map<List<OrderResponseWithDetails>>(paginatedResult.Data);
            
            return new PaginatedResult<OrderResponseWithDetails>(
                mappedData,
                paginatedResult.TotalCount,
                paginatedResult.PageNumber,
                paginatedResult.PageSize);
        }
    }
}
