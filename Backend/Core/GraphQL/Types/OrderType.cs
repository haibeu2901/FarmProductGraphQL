using BusinessObject.Models;

namespace Core.GraphQL.Types
{
    public class OrderType : ObjectType<Order>
    {
        protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
        {
            descriptor.Field(o => o.OrderId).Type<NonNullType<IdType>>();
            descriptor.Field(o => o.StaffId).Type<IntType>();
            descriptor.Field(o => o.CustomerId).Type<IntType>();
            descriptor.Field(o => o.OrderDate).Type<DateTimeType>();
            descriptor.Field(o => o.TotalAmount).Type<NonNullType<DecimalType>>();

            // Navigation properties
            descriptor.Field(o => o.Customer).Type<AccountType>();
            descriptor.Field(o => o.Staff).Type<AccountType>();
            descriptor.Field(o => o.OrderDetails).Type<ListType<OrderDetailType>>();
        }
    }
}
