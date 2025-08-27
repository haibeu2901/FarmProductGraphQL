using BusinessObject.Models;
using BusinessObject.ViewModels.Order;
using Services.Interfaces;
using HotChocolate;
using Core.GraphQL.Inputs;

namespace Core.GraphQL.Mutations
{
    [ExtendObjectType("Mutation")]
    public class OrderMutations
    {
        public async Task<OrderResponseWithDetails?> CreateOrder(
            OrderInput orderInput, 
            List<OrderDetailInput> orderDetailsInput,
            [Service] IOrderService orderService)
        {
            var order = new Order
            {
                StaffId = orderInput.StaffId,
                CustomerId = orderInput.CustomerId,
                OrderDate = orderInput.OrderDate,
                TotalAmount = orderInput.TotalAmount
            };

            var orderDetails = orderDetailsInput.Select(od => new OrderDetail
            {
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                Total = od.Total
            }).ToList();

            var response = await orderService.CreateOrderAsync(order, orderDetails);
            return response.Succeeded ? response.Data : null;
        }

        public async Task<OrderResponseWithDetails?> UpdateOrder(
            int orderId,
            OrderInput orderInput,
            List<OrderDetailInput> orderDetailsInput,
            [Service] IOrderService orderService)
        {
            var order = new Order
            {
                OrderId = orderId,
                StaffId = orderInput.StaffId,
                CustomerId = orderInput.CustomerId,
                OrderDate = orderInput.OrderDate,
                TotalAmount = orderInput.TotalAmount
            };

            var orderDetails = orderDetailsInput.Select(od => new OrderDetail
            {
                DetailId = od.DetailId ?? 0,
                OrderId = orderId,
                ProductId = od.ProductId,
                Quantity = od.Quantity,
                UnitPrice = od.UnitPrice,
                Total = od.Total
            }).ToList();

            var response = await orderService.UpdateOrderAsync(order, orderDetails);
            return response.Succeeded ? response.Data : null;
        }

        public async Task<bool> DeleteOrder(int orderId, [Service] IOrderService orderService)
        {
            var response = await orderService.DeleteOrderAsync(orderId);
            return response.Succeeded;
        }
    }
}