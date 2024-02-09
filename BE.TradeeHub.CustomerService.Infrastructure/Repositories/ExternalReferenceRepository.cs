using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class ExternalReferenceRepository : IExternalReferenceRepository
{
    private readonly MongoDbContext _dbContext;

    public ExternalReferenceRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<(ObjectId Id, string Name)> AddNewExternalReferenceAsync(ExternalReferenceEntity externalReference, CancellationToken ctx)
    {
        await _dbContext.ExternalReferences.InsertOneAsync(externalReference, cancellationToken: ctx);
    
        return (externalReference.Id, externalReference.Name);
    }
}