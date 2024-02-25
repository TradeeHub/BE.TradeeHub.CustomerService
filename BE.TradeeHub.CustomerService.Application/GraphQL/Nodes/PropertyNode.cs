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
}