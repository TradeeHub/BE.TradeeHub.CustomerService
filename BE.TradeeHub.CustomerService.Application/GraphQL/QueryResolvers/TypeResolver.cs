using BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;
using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;

public class TypeResolver
{
    public async Task<IEnumerable<PropertyDbObject>?> GetCustomerProperties(
        [Parent] CustomerDbObject customer,[Service] CustomerPropertiesDataLoader customerPropertiesLoader, 
        CancellationToken ctx)
    {
        if (customer.Properties != null)
        {
            return await customerPropertiesLoader.LoadAsync(customer.Id, ctx);
        }
        
        return null;
    }
    
    public async Task<IEnumerable<CommentDbObject>?> GetCustomerComments([Parent] CustomerDbObject customer,[Service] CustomerCommentsDataLoader customerCommentsDataLoader, 
        CancellationToken ctx)
    {
        if (customer.Comments != null)
        {
            return await customerCommentsDataLoader.LoadAsync(customer.Id, ctx);
        }
        
        return null;
    }
    
    public async Task<IEnumerable<CustomerDbObject>> GetPropertyCustomers([Parent] PropertyDbObject property, [Service] CustomerRepository customerRepository)
    {
        return await customerRepository.GetCustomersByIds(property.Customers);
    }
}