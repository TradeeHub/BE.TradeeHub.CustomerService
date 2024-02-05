using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Mappings;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {
        _customerRepository = customerRepository;
    }

    public async Task<ObjectId> AddNewCustomer(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx)
    {
        var customerEntity = request.ToCustomer(userContext.UserId, userContext.UserId);
        var propertyEntity = request.ToProperty(userContext.UserId, userContext.UserId);
        var commentEntity = request.ToComment(userContext.UserId, userContext.UserId);
        
        return await _customerRepository.AddNewCustomerAsync(customerEntity, new List<PropertyEntity>() { propertyEntity },
            new List<CommentEntity>() { commentEntity }, ctx);
    }
}