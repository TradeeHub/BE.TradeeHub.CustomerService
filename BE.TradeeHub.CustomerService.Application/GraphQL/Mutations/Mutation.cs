using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Mutations;

public class Mutation
{
    public async Task<string> GenerateFakeCustomers([Service] CustomerService customerService, int quantity, CancellationToken ctx)
    {
        await customerService.GenerateFakeCustomers(quantity, ctx);
        return $"Successfully generated {quantity} fake customers.";
    }
    
    public async Task<string> TestMutation([Service] CustomerService customerService, int quantity, CancellationToken ctx)
    {
        return $"Successfully generated {quantity} fake customers.";
    }    
    public async Task<string> AddNewCustomer([Service] CustomerService customerService, AddNewCustomerRequest request, CancellationToken ctx)
    {
        // await customerService.AddNewCustomer(request, ctx);
        return $"Successfully Added a New customer";
    }
}