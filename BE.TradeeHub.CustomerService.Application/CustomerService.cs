using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Mappings;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;
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

    public async Task<AddNewUserResponse> AddNewCustomer(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx)
    {
        var customerEntity = request.ToCustomerEntity(userContext.UserId, userContext.UserId);
    
        // Attempt to create property and comment entities, which may return null
        var propertyEntity = request.ToPropertyEntity(userContext.UserId, userContext.UserId);
        var commentEntity = request.ToCommentEntity(userContext.UserId, userContext.UserId);

        // Initialize lists, adding entities if they are not null
        var properties = propertyEntity != null ? [propertyEntity] : new List<PropertyEntity>();
        var comments = commentEntity != null ? [commentEntity] : new List<CommentEntity>();

        // Pass the entities to the repository method, which now receives empty lists if entities are null
        var (id, customerReferenceNumber) = await _customerRepository.AddNewCustomerAsync(customerEntity, properties, comments, ctx);

        return new AddNewUserResponse
        {
            Id = id,
            CustomerReferenceNumber = customerReferenceNumber
        };
    }
}