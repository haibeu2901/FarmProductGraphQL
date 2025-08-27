using BusinessObject.ViewModels.ProductCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.ViewModels.Product
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public string Unit { get; set; } = null!;
        public decimal SellingPrice { get; set; }
        public string? Description { get; set; }
        public int Quantity { get; set; }
        public ProductCategoryResponse ProductCategory { get; set; } = null!;
    }
}
