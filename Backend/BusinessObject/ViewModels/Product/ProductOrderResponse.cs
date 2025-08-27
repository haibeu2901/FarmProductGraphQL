using BusinessObject.ViewModels.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Product
{
    public class ProductOrderResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public ProductCategoryResponse ProductCategory { get; set; } = null!;
    }
}
