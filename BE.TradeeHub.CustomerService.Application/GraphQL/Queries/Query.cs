using BE.TradeeHub.CustomerService.Application.Extensions;
using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Responses;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using BE.TradeeHub.CustomerService.Domain.Requests;
using HotChocolate.Authorization;
using HotChocolate.Data;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Queries;

[QueryType]
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
        try
        {
            var sortDefinition = Builders<CustomerEntity>.Sort.Descending(x => x.ModifiedAt)
                .Descending(x => x.CreatedAt);
            var query = collection.Find(x => x.UserOwnerId == userContext.UserId).Sort(sortDefinition);
            var executableQuery = query.AsExecutable();

            return executableQuery;
        }
        catch(Exception e)
        {
            throw new Exception("Error while fetching customers", e);
        }
    }

    [Authorize]
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
        [Service] UserContext userContext, ObjectId id, CancellationToken cancellationToken)
    {
        var query = collection.Find(x => x.Id == id && x.UserOwnerId == userContext.UserId).AsExecutable();

        return query;
    }
    
    [Authorize]
    [UseFirstOrDefault]
    public async Task<PropertyEntity> GetPropertyById([Service] IMongoCollection<PropertyEntity> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        var idFilter = Builders<PropertyEntity>.Filter.Eq(x => x.Id, id);
        var ownerFilter = Builders<PropertyEntity>.Filter.Eq(x => x.UserOwnerId, userContext.UserId);
        var combinedFilter = Builders<PropertyEntity>.Filter.And(ownerFilter, idFilter);
        return await collection.Find(combinedFilter).FirstOrDefaultAsync(ctx);
    }

    [Authorize]
    public async Task<ReferenceTrackingResponse> SearchCustomerReferencesAsync(
        [Service] ICustomerService customerService, [Service] UserContext userContext, SearchReferenceRequest request,
        CancellationToken ctx)
    {
        return await customerService.SearchForPotentialReferencesAsync(request, userContext.UserId, ctx);
    }
    
    [Authorize]
    [NodeResolver]
    public static async Task<PropertyEntity?> GetProperty([Service] IMongoCollection<PropertyEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }

    [Authorize]
    [NodeResolver]
    public static async Task<CustomerEntity?> GetCustomer([Service] IMongoCollection<CustomerEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [Authorize]
    [NodeResolver]
    public static async Task<ExternalReferenceEntity?> GetExternalReference([Service] IMongoCollection<ExternalReferenceEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
    
    [Authorize]
    [NodeResolver]
    public static async Task<CommentEntity?> GetComment([Service] IMongoCollection<CommentEntity?> collection,
        [Service] UserContext userContext, ObjectId id, CancellationToken ctx)
    {
        return await EntityFetcher.GetEntityByIdAndOwnerId(collection, id, userContext.UserId, ctx);
    }
}