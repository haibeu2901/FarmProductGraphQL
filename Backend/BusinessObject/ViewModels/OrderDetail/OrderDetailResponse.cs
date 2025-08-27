using BusinessObject.Models;
using BusinessObject.ViewModels.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.OrderDetail
{
    public class OrderDetailResponse
    {
        public int DetailId { get; set; }
        public ProductOrderResponse? Product { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public decimal? Total { get; set; }
    }
}
