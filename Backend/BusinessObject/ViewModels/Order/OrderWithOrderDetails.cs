using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Order
{
    public class OrderWithOrderDetails
    {
        public BusinessObject.Models.Order Order { get; set; }
        public List<BusinessObject.Models.OrderDetail> OrderDetails { get; set; } = new List<BusinessObject.Models.OrderDetail>();
    }
}
