using HotChocolate.Types;
using System;
using System.Collections.Generic;
using BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class PropertyType : ObjectType<PropertyEntity>
{
    protected override void Configure(IObjectTypeDescriptor<PropertyEntity> descriptor)
    {
        descriptor.Field(c => c.Id).ID();

        descriptor.Ignore(x=>x.UserOwnerId);
        descriptor.Ignore(x=>x.CreatedBy);
        descriptor.Ignore(x=>x.ModifiedBy);

        descriptor.Field(p => p.Customers)
            .ResolveWith<TypeResolver>(r => r.GetPropertyCustomers(default!, default!))
            .Type<ListType<CustomerType>>()
            .Description("The customers associated with this property.");
    }
}