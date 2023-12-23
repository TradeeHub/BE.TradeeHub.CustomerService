using BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;
using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class CustomerType : ObjectType<CustomerDbObject>
{
    protected override void Configure(IObjectTypeDescriptor<CustomerDbObject> descriptor)
    {
        descriptor.Field(c => c.Properties)
            .ResolveWith<TypeResolver>(r => r.GetCustomerProperties(default!, default!, default!))
            .Type<ListType<PropertyType>>();
    }
}