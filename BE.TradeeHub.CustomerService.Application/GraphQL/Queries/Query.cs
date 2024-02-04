using BE.TradeeHub.CustomerService.Domain.Entities;
using HotChocolate.Authorization;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Queries;

public class Query
{
    [Authorize]
    [UsePaging(MaxPageSize = 1000)]
    [UseProjection]
    [HotChocolate.Types.UseSorting]
    [HotChocolate.Types.UseFiltering]
    public IExecutable<CustomerEntity> GetCustomers([Service] IMongoCollection<CustomerEntity> collection,
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
    public static async Task<IExecutable<PropertyEntity>> GetProperties(
        [Service] IMongoCollection<PropertyEntity> collection, CancellationToken cancellationToken)
    {
        // Pass cancellationToken to async operations if needed
        return collection.AsExecutable();
    }

    [Authorize]
    [UseFirstOrDefault]
    public IExecutable<CustomerEntity> GetCustomerById([Service] IMongoCollection<CustomerEntity> collection,
        ObjectId id, CancellationToken cancellationToken)
    {
        try
        {
            var temp =  collection.Find(x => x.Id == id).AsExecutable();

            return temp;
        }
        catch (Exception e)
        {
            var tmep = e.Message;
            throw;
        }
    }

    [UseFirstOrDefault]
    public static IExecutable<PropertyEntity> GetPropertyById([Service] IMongoCollection<PropertyEntity> collection,
        ObjectId id, CancellationToken cancellationToken)
    {
        return collection.Find(x => x.Id == id).AsExecutable();
    }
}