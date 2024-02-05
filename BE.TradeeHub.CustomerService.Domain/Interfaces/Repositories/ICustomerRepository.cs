using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

public interface ICustomerRepository
{
    Task<CustomerEntity> GetCustomerByIdAsync(ObjectId customerId);
    Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync();
    Task<IEnumerable<CustomerEntity>> GetCustomersByPropertyIdAsync(ObjectId propertyId);
    Task<IEnumerable<CustomerEntity>> GetCustomersByNameAsync(string name);
    Task<IEnumerable<CustomerEntity>> GetCustomersByAddressAsync(string address);
    Task<IEnumerable<CustomerEntity>> GetCustomersByIdsAsync(IEnumerable<ObjectId> customerIds);
    Task<(ObjectId Id, string CustomerReferenceNumber)> AddNewCustomerAsync(CustomerEntity customer,
        IList<PropertyEntity> properties,
        IList<CommentEntity> comments, CancellationToken ctx);
}