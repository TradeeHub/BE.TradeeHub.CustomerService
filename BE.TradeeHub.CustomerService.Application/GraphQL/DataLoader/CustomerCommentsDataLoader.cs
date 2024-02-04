using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;

public class CustomerCommentsDataLoader : GroupedDataLoader<ObjectId, CommentEntity>
{
    private readonly ICommentRepository _commentRepository;

    public CustomerCommentsDataLoader(IBatchScheduler batchScheduler, ICommentRepository commentRepository, DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _commentRepository = commentRepository;
    }

    protected override async Task<ILookup<ObjectId, CommentEntity>> LoadGroupedBatchAsync(IReadOnlyList<ObjectId> customerIds, CancellationToken cancellationToken)
    {
        // Fetch all properties that are related to the provided customer IDs
        var comments = await _commentRepository.GetCommentsByCustomerId(customerIds, cancellationToken);

        // Group the properties by each customer ID
       return comments?.ToLookup(comment => comment.CustomerId);

    }
}