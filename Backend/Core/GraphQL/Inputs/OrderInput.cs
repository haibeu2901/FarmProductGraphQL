using HotChocolate.Types;

namespace Core.GraphQL.Inputs
{
    public class OrderInput
    {
        public int? StaffId { get; set; }
        public int? CustomerId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
    }
}