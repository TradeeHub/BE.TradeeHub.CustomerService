using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task<ObjectId> AddNewCustomer(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx);
}