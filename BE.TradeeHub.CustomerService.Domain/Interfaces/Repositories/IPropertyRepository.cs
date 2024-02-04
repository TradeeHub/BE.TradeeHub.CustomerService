using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

public interface IPropertyRepository
{
    Task<IEnumerable<PropertyEntity>?> GetPropertiesByCustomerIds(IEnumerable<ObjectId> customerIds, CancellationToken ctx);
    Task<IEnumerable<PropertyEntity>?> GetPropertiesByIds(IEnumerable<ObjectId> propertyIds, CancellationToken ctx);
}