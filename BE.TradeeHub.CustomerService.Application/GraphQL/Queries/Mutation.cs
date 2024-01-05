namespace BE.TradeeHub.CustomerService.Application.GraphQL.Queries;

public class Mutation
{
    public async Task<string> GenerateFakeCustomers([Service] CustomerService customerService, int quantity, CancellationToken ctx)
    {
        await customerService.GenerateFakeCustomers(quantity, ctx);
        return $"Successfully generated {quantity} fake customers.";
    }
}
