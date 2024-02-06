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
        [Service] UserContext userContext, CancellationToken cancellationToken)
    {
        var sortDefinition = Builders<CustomerEntity>.Sort.Descending(x => x.ModifiedAt).Descending(x => x.CreatedAt);
        var query = collection.Find(x => x.UserOwnerId == userContext.UserId).Sort(sortDefinition);
        var executableQuery = query.AsExecutable();

        return executableQuery;
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
    public IExecutable<CustomerEntity> GetCustomerById([Service] IMongoCollection<CustomerEntity> collection, [Service] UserContext userContext, ObjectId id, CancellationToken cancellationToken)
    {
        try
        {
            var query = collection.Find(x => x.Id == id && x.UserOwnerId == userContext.UserId).AsExecutable();

            return query;
        }
        catch (Exception e)
        {
            var temp = e.Message;
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