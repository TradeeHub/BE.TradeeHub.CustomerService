using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Nodes;

[Node]
[ExtendObjectType(typeof(ExternalReferenceEntity))]
public static class ExternalReferenceNode
{
    [DataLoader]
    internal static async Task<ILookup<ObjectId, ExternalReferenceEntity>> GetExternalReferenceGroupedByIdAsync(
        IReadOnlyList<ObjectId> externalReferenceIds,
        IMongoCollection<ExternalReferenceEntity> externalReferences,
        CancellationToken cancellationToken)
    {
        var filter = Builders<ExternalReferenceEntity>.Filter.In(m => m.Id, externalReferenceIds);
        var externalReferenceList = await externalReferences.Find(filter).ToListAsync(cancellationToken);

        return externalReferenceList.ToLookup(externalReference => externalReference.Id);
    }
}