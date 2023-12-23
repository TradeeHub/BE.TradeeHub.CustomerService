using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class PropertyRepository
{
    private readonly MongoDbContext _dbContext;

    public PropertyRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<PropertyDbObject>?> GetPropertiesByCustomerIds(IEnumerable<ObjectId> customerIds, CancellationToken ctx)
    {
        // The filter should be on the Customers field, not the Id field
        var filter = Builders<PropertyDbObject>.Filter.AnyIn(p => p.Customers, customerIds);
        var cursor = await _dbContext.Properties.FindAsync(filter, cancellationToken: ctx);
        var properties = await cursor.ToListAsync(ctx);

        return properties; 
    }
    
    public async Task<IEnumerable<PropertyDbObject>?> GetPropertiesByIds(IEnumerable<ObjectId> propertyIds, CancellationToken ctx)
    {
        var filter = Builders<PropertyDbObject>.Filter.In(c => c.Id, propertyIds);
        var cursor = await _dbContext.Properties.FindAsync(filter, cancellationToken: ctx);
        var temp=  await cursor.ToListAsync(ctx);

        return temp; 
    }
}