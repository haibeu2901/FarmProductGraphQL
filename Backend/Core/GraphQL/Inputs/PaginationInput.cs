namespace Core.GraphQL.Inputs
{
    public class PaginationInput
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}