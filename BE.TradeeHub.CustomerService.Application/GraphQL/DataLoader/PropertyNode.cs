using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using MongoDB.Bson;
using BE.TradeeHub.CustomerService.Infrastructure;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Driver;

[ExtendObjectType<PropertyEntity>]
public static class PropertyNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, PropertyEntity>>
        GetPropertyByCustomerIdAsync(IReadOnlyList<ObjectId> customerIds, [Service] IPropertyRepository propertyRepository, CancellationToken ctx)
    {
        var properties = await propertyRepository.GetPropertiesByCustomerIds(customerIds, ctx);

        // Group the properties by each customer ID
        return customerIds
            .SelectMany(customerId => 
                properties.Where(p => p.Customers.Contains(customerId))
                    .Select(p => new { CustomerId = customerId, Property = p }))
            .ToLookup(x => x.CustomerId, x => x.Property);
    }
}