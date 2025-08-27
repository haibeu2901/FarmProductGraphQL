using BusinessObject.ViewModels.Pagination;

namespace Core.GraphQL.Types
{
    public class PaginatedResultType<T> : ObjectType<PaginatedResult<T>>
        where T : class
    {
        protected override void Configure(IObjectTypeDescriptor<PaginatedResult<T>> descriptor)
        {
            descriptor.Field(p => p.Data)
                .Type<ListType<NonNullType<ObjectType<T>>>>();
            
            descriptor.Field(p => p.TotalCount)
                .Type<NonNullType<IntType>>();
            
            descriptor.Field(p => p.PageNumber)
                .Type<NonNullType<IntType>>();
            
            descriptor.Field(p => p.PageSize)
                .Type<NonNullType<IntType>>();
            
            descriptor.Field(p => p.TotalPages)
                .Type<NonNullType<IntType>>();
            
            descriptor.Field(p => p.HasNextPage)
                .Type<NonNullType<BooleanType>>();
            
            descriptor.Field(p => p.HasPreviousPage)
                .Type<NonNullType<BooleanType>>();
        }
    }
}