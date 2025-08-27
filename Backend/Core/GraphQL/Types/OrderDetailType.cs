using BusinessObject.Models;
using HotChocolate.Types;

namespace Core.GraphQL.Types
{
    public class OrderDetailType : ObjectType<OrderDetail>
    {
        protected override void Configure(IObjectTypeDescriptor<OrderDetail> descriptor)
        {
            descriptor.Field(od => od.DetailId).Type<NonNullType<IdType>>();
            descriptor.Field(od => od.OrderId).Type<IntType>();
            descriptor.Field(od => od.ProductId).Type<IntType>();
            descriptor.Field(od => od.Quantity).Type<NonNullType<IntType>>();
            descriptor.Field(od => od.UnitPrice).Type<NonNullType<DecimalType>>();
            descriptor.Field(od => od.Total).Type<DecimalType>();
            
            // Navigation properties
            descriptor.Field(od => od.Order).Type<OrderType>();
            descriptor.Field(od => od.Product).Type<ProductType>();
        }
    }
}