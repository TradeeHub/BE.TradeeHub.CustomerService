using BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;
using BE.TradeeHub.CustomerService.Domain.Entities;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class ReferenceInfoType : ObjectType<ReferenceInfoEntity>
{
    protected override void Configure(IObjectTypeDescriptor<ReferenceInfoEntity> descriptor)
    {
        descriptor.Field(r=> r.ExternalReference)
            .ResolveWith<TypeResolver>(r => r.GetExternalReference(default!, default!, default!))
            .Type<ExternalReferenceType>();

        descriptor.Field(r=> r.Customer)
            .ResolveWith<TypeResolver>(r => r.GetCustomer(default!, default!, default!))
            .Type<CustomerType>();
    }
}
