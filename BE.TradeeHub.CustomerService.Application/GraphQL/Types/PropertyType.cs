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
        descriptor.Field(p => p.Customers)
            .ResolveWith<TypeResolver>(r => r.GetPropertyCustomers(default!, default!))
            .Type<ListType<CustomerType>>()
            .Description("The customers associated with this property.");
    }
}