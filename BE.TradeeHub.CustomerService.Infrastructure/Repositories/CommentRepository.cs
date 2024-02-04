using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class CommentRepository
{
    private readonly MongoDbContext _dbContext;

    public CommentRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<CommentDbObject>?> GetCommentsByCustomerId(IEnumerable<ObjectId> customerIds, CancellationToken cancellationToken)
    {
        // Assuming CustomerId is the correct field to filter on based on your class definition
        var filter = Builders<CommentDbObject>.Filter.In(comment => comment.CustomerId, customerIds);
        var cursor = await _dbContext.Comments.FindAsync(filter, cancellationToken: cancellationToken);
        var comments = await cursor.ToListAsync(cancellationToken);

        return comments; 
    }

    
    public async Task<IEnumerable<CommentDbObject>?> GetCommentsByIds(IEnumerable<ObjectId> commentIds, CancellationToken ctx)
    {
        var filter = Builders<CommentDbObject>.Filter.In(c => c.Id, commentIds);
        var cursor = await _dbContext.Comments.FindAsync(filter, cancellationToken: ctx);
        var comments=  await cursor.ToListAsync(ctx);

        return comments; 
    }
}