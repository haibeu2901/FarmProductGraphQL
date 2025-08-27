using BusinessObject.Models;
using HotChocolate.Types;

namespace Core.GraphQL.Types
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            // Add fields based on your Product model
            // This is a placeholder - adjust according to your actual Product model
            descriptor.Field("id").Resolve(context => context.Parent<Product>().ProductId);
        }
    }
}