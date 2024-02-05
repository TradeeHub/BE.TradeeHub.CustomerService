

using BE.TradeeHub.CustomerService.Application.Extensions;
using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

namespace BE.TradeeHub.CustomerService.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task AddNewCustomer(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx)
    {
        var customerEntity = request.ToCustomer(userContext.UserId, userContext.UserId);
        var propertyEntity = request.ToProperty(userContext.UserId, userContext.UserId);
        var commentEntity = request.ToComment(userContext.UserId, userContext.UserId);
        var temp = request;
        await _customerRepository.AddNewCustomerAsync(customerEntity, new List<PropertyEntity>() { propertyEntity },
            new List<CommentEntity>() { commentEntity }, ctx);
    }
}