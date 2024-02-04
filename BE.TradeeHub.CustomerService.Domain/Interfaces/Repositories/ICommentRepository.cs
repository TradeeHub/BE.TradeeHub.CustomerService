using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

public interface ICommentRepository
{
    Task<IEnumerable<CommentEntity>?> GetCommentsByCustomerId(IEnumerable<ObjectId> customerIds, CancellationToken cancellationToken);
    Task<IEnumerable<CommentEntity>?> GetCommentsByIds(IEnumerable<ObjectId> commentIds, CancellationToken ctx);
}