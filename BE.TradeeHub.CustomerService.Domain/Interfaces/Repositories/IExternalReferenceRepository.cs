using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using BE.TradeeHub.CustomerService.Domain.Results;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

public interface IExternalReferenceRepository
{
    Task<(ObjectId Id, string Name)> AddNewExternalReferenceAsync(ExternalReferenceEntity customer, CancellationToken ctx);
    Task<ExternalReferencePageResult> SearchExternalReferencesAsync(string searchTerm, string? lastCursor, int pageSize, Guid userId, CancellationToken ctx);
}