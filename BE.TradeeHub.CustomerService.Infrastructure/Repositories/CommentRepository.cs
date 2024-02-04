using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class CommentRepository : ICommentRepository
{
    private readonly MongoDbContext _dbContext;

    public CommentRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<IEnumerable<CommentEntity>?> GetCommentsByCustomerId(IEnumerable<ObjectId> customerIds, CancellationToken cancellationToken)
    {
        // Assuming CustomerId is the correct field to filter on based on your class definition
        var filter = Builders<CommentEntity>.Filter.In(comment => comment.CustomerId, customerIds);
        var cursor = await _dbContext.Comments.FindAsync(filter, cancellationToken: cancellationToken);
        var comments = await cursor.ToListAsync(cancellationToken);

        return comments; 
    }

    
    public async Task<IEnumerable<CommentEntity>?> GetCommentsByIds(IEnumerable<ObjectId> commentIds, CancellationToken ctx)
    {
        var filter = Builders<CommentEntity>.Filter.In(c => c.Id, commentIds);
        var cursor = await _dbContext.Comments.FindAsync(filter, cancellationToken: ctx);
        var comments=  await cursor.ToListAsync(ctx);

        return comments; 
    }
}