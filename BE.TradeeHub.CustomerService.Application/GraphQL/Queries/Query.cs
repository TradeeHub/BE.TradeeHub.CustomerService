using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using HotChocolate.Authorization;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Queries;

// [QueryType]
public class Query
{
    [Authorize]
    [UsePaging(MaxPageSize = 1000)]
    [UseProjection]
    [HotChocolate.Types.UseSorting]
    [HotChocolate.Types.UseFiltering]
    public IExecutable<CustomerDbObject> GetCustomers([Service] IMongoCollection<CustomerDbObject> collection,
        CancellationToken cancellationToken)
    {
        var collect = collection.AsExecutable();

        // Use cancellationToken in any cancellable operation here
        // For example, pass it to database queries if supported

        return collect;
    }
 
    [UsePaging]
    [UseProjection]
    [HotChocolate.Types.UseSorting]
    [HotChocolate.Types.UseFiltering]
    public static async Task<IExecutable<PropertyDbObject>> GetProperties(
        [Service] IMongoCollection<PropertyDbObject> collection, CancellationToken cancellationToken)
    {
        // Pass cancellationToken to async operations if needed
        return collection.AsExecutable();
    }

    [Authorize]
    [UseFirstOrDefault]
    public IExecutable<CustomerDbObject> GetCustomerById([Service] IMongoCollection<CustomerDbObject> collection,
        ObjectId id, CancellationToken cancellationToken)
    {
        return collection.Find(x => x.Id == id).AsExecutable();
    }

    [UseFirstOrDefault]
    public static IExecutable<PropertyDbObject> GetPropertyById([Service] IMongoCollection<PropertyDbObject> collection,
        ObjectId id, CancellationToken cancellationToken)
    {
        return collection.Find(x => x.Id == id).AsExecutable();
    }
}