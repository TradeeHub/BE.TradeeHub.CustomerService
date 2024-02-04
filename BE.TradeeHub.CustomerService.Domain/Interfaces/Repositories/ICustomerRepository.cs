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
    Task AddNewCustomerAsync(CustomerEntity customer, IEnumerable<PropertyEntity> properties, IEnumerable<CommentEntity> comments, CancellationToken ctx);
}