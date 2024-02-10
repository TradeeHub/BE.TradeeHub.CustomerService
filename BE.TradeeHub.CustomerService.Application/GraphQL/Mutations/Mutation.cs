using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Requests;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Mutations;

public class Mutation
{
    public async Task<AddNewCustomerResponse> AddNewCustomerAsync([Service] ICustomerService customerService, [Service] UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx)
    {
        return await customerService.AddNewCustomerAsync(userContext, request, ctx);
    }
    
    public async Task<AddNewExternalReferenceResponse> AddNewExternalReferenceAsync([Service] ICustomerService customerService, [Service] UserContext userContext, AddNewExternalReferenceRequest request, CancellationToken ctx)
    {
        return await customerService.AddNewExternalReferenceAsync(userContext, request, ctx);
    }
}