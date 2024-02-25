using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(CommentEntity))]
public static class CommentNode
{
    public static async Task<CustomerEntity?> GetCustomer([Parent] CommentEntity comment,
        ICustomerGroupedByIdDataLoader customerGroupedByIdDataLoader, CancellationToken ctx)
    {
        if ( comment.CustomerId == ObjectId.Empty)
        {
            return null;
        }

        var customers = await customerGroupedByIdDataLoader.LoadAsync(comment.CustomerId, ctx);

        return customers.FirstOrDefault();
    }
    
    [DataLoader]
    internal static async Task<ILookup<ObjectId, CommentEntity>> GetCommentGroupedByIdAsync(
        IReadOnlyList<ObjectId> commentIds,
        IMongoCollection<CommentEntity> comments,
        CancellationToken cancellationToken)
    {
        var filter = Builders<CommentEntity>.Filter.In(m => m.Id, commentIds);
        var commentList = await comments.Find(filter).ToListAsync(cancellationToken);

        return commentList.ToLookup(comment => comment.Id);
    }
}