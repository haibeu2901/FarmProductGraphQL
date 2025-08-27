using BusinessObject.Models;
using BusinessObject.ViewModels.Account;
using BusinessObject.ViewModels.OrderDetail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Order
{
    public class OrderResponseWithDetails
    {
        public int OrderId { get; set; }
        public AccountOrderResponse? Customer { get; set; }
        public AccountOrderResponse? Staff { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderDetailResponse> OrderDetails { get; set; } = new List<OrderDetailResponse>();
    }
}
