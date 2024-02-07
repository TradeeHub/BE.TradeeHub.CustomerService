using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;
using BE.TradeeHub.CustomerService.Domain.Entities;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task<AddNewCustomerResponse> AddNewCustomer(UserContext userContext, AddNewCustomerRequest request,
        CancellationToken ctx);

    Task<List<CustomerEntity>> SearchCustomersAsync(string searchTerm, Guid userId, CancellationToken ctx);
}