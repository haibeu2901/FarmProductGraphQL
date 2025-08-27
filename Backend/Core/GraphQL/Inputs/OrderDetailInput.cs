namespace Core.GraphQL.Inputs
{
    public class OrderDetailInput
    {
        public int? DetailId { get; set; }
        public int? ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal? Total { get; set; }
    }
}