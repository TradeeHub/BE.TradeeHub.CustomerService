using BE.TradeeHub.CustomerService.Infrastructure.Repositories;

namespace BE.TradeeHub.CustomerService.Application;

public class CustomerService
{
    private readonly CustomerRepository _customerRepository;

    public CustomerService(CustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task GenerateFakeCustomers(int quantity, CancellationToken ctx)
    {
        await _customerRepository.GenerateFakeData(quantity, ctx);
    }
}