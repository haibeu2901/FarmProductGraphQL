using BusinessObject.Models;

namespace Core.GraphQL.Types
{
    public class AccountType : ObjectType<Account>
    {
        protected override void Configure(IObjectTypeDescriptor<Account> descriptor)
        {
            descriptor.Field("id").Resolve(context => context.Parent<Account>().AccountId);
        }
    }
}
