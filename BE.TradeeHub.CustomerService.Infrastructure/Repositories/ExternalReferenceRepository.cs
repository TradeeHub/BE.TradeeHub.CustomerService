using System.Text.RegularExpressions;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Domain.Results;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class ExternalReferenceRepository : IExternalReferenceRepository
{
    private readonly MongoDbContext _dbContext;

    public ExternalReferenceRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<(ObjectId Id, string Name)> AddNewExternalReferenceAsync(
        ExternalReferenceEntity externalReference, CancellationToken ctx)
    {
        await _dbContext.ExternalReferences.InsertOneAsync(externalReference, cancellationToken: ctx);

        return (externalReference.Id, externalReference.Name);
    }
    
    public async Task<ExternalReferenceEntity?> GetExternalReferenceByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
        var filter = Builders<ExternalReferenceEntity>.Filter.Eq(er => er.Id, id);
        var result = await _dbContext.ExternalReferences.Find(filter).FirstOrDefaultAsync(cancellationToken);
        return result;
    }

    public async Task<ExternalReferencePageResult> SearchExternalReferencesAsync(string searchTerm, string? lastCursor,
        int pageSize, Guid userId, CancellationToken ctx)
    {
        try
        {
            var objectIdLastCursor = string.IsNullOrEmpty(lastCursor) ? (ObjectId?)null : new ObjectId(lastCursor);

            // Search Filter Stage
            var matchStage = new BsonDocument("$match", new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("UserOwnerId", userId),
                new BsonDocument("$or", new BsonArray
                {
                    new BsonDocument("Name", new BsonRegularExpression(Regex.Escape(searchTerm), "i")),
                    new BsonDocument("CompanyName", new BsonRegularExpression(Regex.Escape(searchTerm), "i")),
                    new BsonDocument("PhoneNumber.Number", new BsonRegularExpression(Regex.Escape(searchTerm), "i")),
                    new BsonDocument("Email.Address", new BsonRegularExpression(Regex.Escape(searchTerm), "i")),
                    new BsonDocument("Place.Address", new BsonRegularExpression(Regex.Escape(searchTerm), "i"))
                })
            }));

            if (objectIdLastCursor != null)
            {
                matchStage["$match"]["$and"].AsBsonArray
                    .Add(new BsonDocument("_id", new BsonDocument("$gt", objectIdLastCursor)));
            }

            var pipeline = new[]
            {
                matchStage,
                new BsonDocument("$sort", new BsonDocument("_id", 1)), // Sort by _id ascending
                new BsonDocument("$limit", pageSize + 1) // Fetch one extra record to determine if there's a next page
            };

            var externalReferences = await _dbContext.ExternalReferences
                .Aggregate<ExternalReferenceEntity>(pipeline, cancellationToken: ctx).ToListAsync(ctx);

            // Pagination Logic
            var hasNextPage = externalReferences.Count > pageSize;
            var returnedExternalReferences =
                hasNextPage ? externalReferences.Take(pageSize).ToList() : externalReferences;
            var nextCursor = hasNextPage ? returnedExternalReferences.Last().Id.ToString() : null;

            return new ExternalReferencePageResult
            {
                ExternalReferences = returnedExternalReferences,
                HasNextPage = hasNextPage,
                NextCursor = nextCursor
            };
        }
        catch (Exception ex)
        {
            // Handle the exception appropriately
            return new ExternalReferencePageResult
            {
                ExternalReferences = new List<ExternalReferenceEntity>(),
                HasNextPage = false,
                NextCursor = null
            };
        }
    }
}