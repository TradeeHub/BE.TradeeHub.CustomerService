using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;

public class CustomerCommentsDataLoader : GroupedDataLoader<ObjectId, CommentDbObject>
{
    private readonly CommentRepository _commentRepository;

    public CustomerCommentsDataLoader(IBatchScheduler batchScheduler, CommentRepository commentRepository, DataLoaderOptions? options = null)
        : base(batchScheduler, options)
    {
        _commentRepository = commentRepository;
    }

    protected override async Task<ILookup<ObjectId, CommentDbObject>> LoadGroupedBatchAsync(IReadOnlyList<ObjectId> customerIds, CancellationToken cancellationToken)
    {
        // Fetch all properties that are related to the provided customer IDs
        var comments = await _commentRepository.GetCommentsByCustomerId(customerIds, cancellationToken);

        // Group the properties by each customer ID
       return comments?.ToLookup(comment => comment.CustomerId);

    }
}