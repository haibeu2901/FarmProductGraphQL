using BusinessObject.Models;
using BusinessObject.ViewModels.Order;
using BusinessObject.ViewModels.Pagination;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            var response = await _orderService.GetAllOrdersAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetOrderById")]
        public async Task<IActionResult> GetOrderById([FromQuery] int orderId)
        {
            var response = await _orderService.GetOrderByIdAsync(orderId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPost("CreateOrder")]
        public async Task<IActionResult> CreateOrder([FromBody] OrderWithOrderDetails orderWithDetails)
        {
            var response = await _orderService.CreateOrderAsync(orderWithDetails.Order, orderWithDetails.OrderDetails);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> UpdateOrder([FromBody] OrderWithOrderDetails orderWithDetails)
        {
            var response = await _orderService.UpdateOrderAsync(orderWithDetails.Order, orderWithDetails.OrderDetails);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpDelete("DeleteOrder")]
        public async Task<IActionResult> DeleteOrder([FromQuery]int orderId)
        {
            var response = await _orderService.DeleteOrderAsync(orderId);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetOrdersPaginated")]
        public async Task<IActionResult> GetOrdersPaginated([FromQuery] PaginationRequest request)
        {
            var response = await _orderService.GetOrdersPaginatedAsync(request);
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("GetLatestOrder")]
        public async Task<IActionResult> GetLatestOrder()
        {
            var response = await _orderService.GetLatestOrderAsync();
            if (!response.Succeeded)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
