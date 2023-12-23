using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using MongoDB.Bson;
using BE.TradeeHub.CustomerService.Infrastructure;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Driver;

[ExtendObjectType<PropertyDbObject>]
public static class PropertyNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, PropertyDbObject>>
        GetPropertyByCustomerIdAsync(IReadOnlyList<ObjectId> customerIds, [Service] PropertyRepository propertyRepository, CancellationToken ctx)
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