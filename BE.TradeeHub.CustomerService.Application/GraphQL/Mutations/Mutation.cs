using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Mutations;

public class Mutation
{
    public async Task<ObjectId> AddNewCustomer([Service] ICustomerService customerService, [Service] UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx)
    {
        return await customerService.AddNewCustomer(userContext, request, ctx);
    }
}