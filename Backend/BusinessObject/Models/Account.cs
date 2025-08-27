using System;
using System.Collections.Generic;

namespace BusinessObject.Models;

public partial class Account
{
    public int AccountId { get; set; }

    public string FullName { get; set; } = null!;

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte Role { get; set; }

    public string? PhoneNumber { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public DateTime? CreatedDate { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<ImportedStock> ImportedStocks { get; set; } = new List<ImportedStock>();

    public virtual ICollection<Order> OrderCustomers { get; set; } = new List<Order>();

    public virtual ICollection<Order> OrderStaffs { get; set; } = new List<Order>();
}
