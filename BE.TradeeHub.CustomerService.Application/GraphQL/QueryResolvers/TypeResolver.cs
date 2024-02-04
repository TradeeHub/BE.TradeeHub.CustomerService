using BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;

public class TypeResolver
{
    public async Task<IEnumerable<PropertyEntity>?> GetCustomerProperties(
        [Parent] CustomerEntity customer,[Service] CustomerPropertiesDataLoader customerPropertiesLoader, 
        CancellationToken ctx)
    {
        if (customer.Properties != null)
        {
            return await customerPropertiesLoader.LoadAsync(customer.Id, ctx);
        }
        
        return null;
    }
    
    public async Task<IEnumerable<CommentEntity>?> GetCustomerComments([Parent] CustomerEntity customer,[Service] CustomerCommentsDataLoader customerCommentsDataLoader, 
        CancellationToken ctx)
    {
        if (customer.Comments != null)
        {
            return await customerCommentsDataLoader.LoadAsync(customer.Id, ctx);
        }
        
        return null;
    }
    
    public async Task<IEnumerable<CustomerEntity>> GetPropertyCustomers([Parent] PropertyEntity property, [Service] ICustomerRepository customerRepository)
    {
        return await customerRepository.GetCustomersByIdsAsync(property.Customers);
    }
}