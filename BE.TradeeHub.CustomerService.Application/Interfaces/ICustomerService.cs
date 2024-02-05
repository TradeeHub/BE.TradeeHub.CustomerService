using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task<AddNewUserResponse> AddNewCustomer(UserContext userContext, AddNewCustomerRequest request,
        CancellationToken ctx);
}