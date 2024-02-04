using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task AddNewCustomer(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx);
}