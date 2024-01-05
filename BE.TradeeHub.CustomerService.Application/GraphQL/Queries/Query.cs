using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Queries;

// [QueryType]
public class Query
{
    [UsePaging(MaxPageSize = 1000)]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public IExecutable<CustomerDbObject> GetCustomers([Service] IMongoCollection<CustomerDbObject> collection,
        CancellationToken cancellationToken)
    {
        var collect = collection.AsExecutable();

        // Use cancellationToken in any cancellable operation here
        // For example, pass it to database queries if supported

        return collect;
    }
    public async Task<IEnumerable<CustomerDbObject>>? GetAllCustomers([Service] CustomerRepository customerRepository, CancellationToken cancellationToken)
    {
        // Pass cancellationToken to async operations within the repository
        return await customerRepository.GetAllCustomers();
    }
    
    public async Task<CustomerDbObject>? GetCustomerByIdCustom([Service] CustomerRepository customerRepository,
        ObjectId customerId, CancellationToken cancellationToken)
    {
        // Pass cancellationToken to async operations within the repository
        return await customerRepository.GetCustomerById(customerId);
    }
    
    [UsePaging]
    [UseProjection]
    [UseSorting]
    [UseFiltering]
    public static async Task<IExecutable<PropertyDbObject>> GetProperties(
        [Service] IMongoCollection<PropertyDbObject> collection, CancellationToken cancellationToken)
    {
        // Pass cancellationToken to async operations if needed
        return collection.AsExecutable();
    }

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