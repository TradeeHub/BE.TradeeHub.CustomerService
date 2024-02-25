using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(PropertyEntity))]
public static class PropertyNode
{
    public static async Task<List<CustomerEntity>> GetCustomers([Parent] PropertyEntity property,
        ICustomerGroupedByIdDataLoader customerGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (property.CustomerIds.Count == 0)
        {
            return [];
        }

        var customerGroups = await customerGroupedByIdDataLoader.LoadAsync(property.CustomerIds, ctx);

        var customers = customerGroups.SelectMany(group => group).ToList();

        return customers;
    }
    
    [DataLoader]
    internal static async Task<ILookup<ObjectId, PropertyEntity>> GetPropertyGroupedByIdAsync(
        IReadOnlyList<ObjectId> propertyIds,
        IMongoCollection<PropertyEntity> properties,
        CancellationToken cancellationToken)
    {
        var filter = Builders<PropertyEntity>.Filter.In(m => m.Id, propertyIds);
        var propertyList = await properties.Find(filter).ToListAsync(cancellationToken);

        return propertyList.ToLookup(property => property.Id);
    }
    
    /// <summary>
    /// Many To Many relationship between Customer and Property user for customers for grid 
    /// </summary>
    /// <param name="customerIds"></param>
    /// <param name="properties"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [DataLoader]
    internal static async Task<ILookup<ObjectId, PropertyEntity>> GetPropertyGroupedByCustomerIdAsync(
        IReadOnlyList<ObjectId> customerIds,
        IMongoCollection<PropertyEntity> properties,
        CancellationToken cancellationToken)
    {
        var filter = Builders<PropertyEntity>.Filter.AnyIn(m => m.CustomerIds, customerIds);
        var propertyList = await properties.Find(filter).ToListAsync(cancellationToken);

        return customerIds
            .SelectMany(customerId => 
                propertyList.Where(p => p.CustomerIds.Contains(customerId))
                    .Select(p => new { CustomerId = customerId, Property = p }))
            .ToLookup(x => x.CustomerId, x => x.Property);
    }
}