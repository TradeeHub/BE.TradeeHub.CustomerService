using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;

[ExtendObjectType<CommentEntity>]
public static class CommentNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, CommentEntity>?> GetCommentByCustomerIdAsync(IReadOnlyList<ObjectId> customerIds, [Service] ICommentRepository commentRepository, CancellationToken ctx)
    {
        var comments = await commentRepository.GetCommentsByCustomerId(customerIds, ctx);

        return comments?.ToLookup(comment => comment.CustomerId);
    }
}