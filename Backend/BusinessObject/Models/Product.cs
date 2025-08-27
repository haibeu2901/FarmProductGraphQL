using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public string ProductName { get; set; } = null!;

    public string Unit { get; set; } = null!;

    public decimal SellingPrice { get; set; }

    public string? Description { get; set; }

    public int Quantity { get; set; }

    public virtual ProductCategory? ProductCategory { get; set; }

    public virtual ICollection<ImportedStock> ImportedStocks { get; set; } = new List<ImportedStock>();

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
}
