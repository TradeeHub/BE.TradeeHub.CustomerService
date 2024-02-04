using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Mutations;

public class Mutation
{
    public async Task<string> AddNewCustomer([Service] ICustomerService customerService, [Service] UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx)
    {
        await customerService.AddNewCustomer(userContext, request, ctx);
        return $"Successfully Added a New customer";
    }
}