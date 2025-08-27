using BusinessObject.ViewModels.Pagination;

namespace Services.Responses
{
    public class PaginatedApiResponse<T> : ApiResponse<PaginatedResult<T>>
    {
        public PaginatedApiResponse()
        {
        }

        public PaginatedApiResponse(PaginatedResult<T> data) : base(data)
        {
        }

        public PaginatedApiResponse(List<T> items, int totalCount, int pageNumber, int pageSize)
        {
            Data = new PaginatedResult<T>(items, totalCount, pageNumber, pageSize);
            Succeeded = true;
            Message = string.Empty;
            Errors = null;
        }
    }
}