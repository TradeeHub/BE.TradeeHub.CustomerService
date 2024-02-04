using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;

[ExtendObjectType<CommentDbObject>]
public static class CommentNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, CommentDbObject>?> GetCommentByCustomerIdAsync(IReadOnlyList<ObjectId> customerIds, [Service] CommentRepository commentRepository, CancellationToken ctx)
    {
        var comments = await commentRepository.GetCommentsByCustomerId(customerIds, ctx);

        return comments?.ToLookup(comment => comment.CustomerId);
    }
}