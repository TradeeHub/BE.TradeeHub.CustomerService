using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

public interface IExternalReferenceRepository
{
    Task<(ObjectId Id, string Name)> AddNewExternalReferenceAsync(ExternalReferenceEntity customer, CancellationToken ctx);
}