using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(CustomerEntity))]
public static class CustomerNode
{
    public static async Task<List<PropertyEntity>> GetProperties([Parent] CustomerEntity customer,
        IPropertyGroupedByIdDataLoader propertyGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (customer.PropertyIds == null || customer.PropertyIds.Count == 0)
        {
            return [];
        }

        var propertyGroups = await propertyGroupedByIdDataLoader.LoadAsync(customer.PropertyIds, ctx);

        var properties = propertyGroups.SelectMany(group => group).ToList();

        return properties;
    }
    
    public static async Task<List<CommentEntity>> GetComments([Parent] CustomerEntity customer,
        ICommentGroupedByIdDataLoader customerGroupedByIdDataLoader, CancellationToken ctx)
    {
        if (customer.CommentIds == null || customer.CommentIds.Count == 0)
        {
            return [];
        }

        var commentGroups = await customerGroupedByIdDataLoader.LoadAsync(customer.CommentIds, ctx);

        var comments = commentGroups.SelectMany(group => group).ToList();

        return comments;
    }
    
    [DataLoader]
    internal static async Task<ILookup<ObjectId, CustomerEntity>> GetCustomerGroupedByIdAsync(
        IReadOnlyList<ObjectId> customerIds,
        IMongoCollection<CustomerEntity> customers,
        CancellationToken cancellationToken)
    {
        var filter = Builders<CustomerEntity>.Filter.In(m => m.Id, customerIds);
        var customerList = await customers.Find(filter).ToListAsync(cancellationToken);

        return customerList.ToLookup(customer => customer.Id);
    }
}