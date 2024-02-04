using BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;
using BE.TradeeHub.CustomerService.Domain.Entities;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class CustomerType : ObjectType<CustomerEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CustomerEntity> descriptor)
    {
        descriptor.Field(c => c.Properties)
            .ResolveWith<TypeResolver>(r => r.GetCustomerProperties(default!, default!, default!))
            .Type<ListType<PropertyType>>();

        descriptor.Field(c => c.Comments)
            .ResolveWith<TypeResolver>(r => r.GetCustomerComments(default!, default!, default!))
            .Type<ListType<CommentType>>();
    }
}