using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Mappings;
using BE.TradeeHub.CustomerService.Application.Requests;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Enums;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;

namespace BE.TradeeHub.CustomerService.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IExternalReferenceRepository _externalReferenceRepository;

    public CustomerService(ICustomerRepository customerRepository, IExternalReferenceRepository externalReferenceRepository)
    {
        _externalReferenceRepository = externalReferenceRepository;
        _customerRepository = customerRepository;
    }

    public async Task<AddNewCustomerResponse> AddNewCustomerAsync(UserContext userContext, AddNewCustomerRequest request,
        CancellationToken ctx)
    {
        var customerEntity = request.ToCustomerEntity(userContext.UserId, userContext.UserId);

        // Attempt to create property and comment entities, which may return null
        var propertyEntity = request.ToPropertyEntity(userContext.UserId, userContext.UserId);
        var commentEntity = request.ToCommentEntity(userContext.UserId, userContext.UserId);

        // Initialize lists, adding entities if they are not null
        var properties = propertyEntity != null ? [propertyEntity] : new List<PropertyEntity>();
        var comments = commentEntity != null ? [commentEntity] : new List<CommentEntity>();

        // Pass the entities to the repository method, which now receives empty lists if entities are null
        var (id, customerReferenceNumber) =
            await _customerRepository.AddNewCustomerAsync(customerEntity, properties, comments, ctx);

        return new AddNewCustomerResponse
        {
            Id = id,
            CustomerReferenceNumber = customerReferenceNumber
        };
    }

    public async Task<ReferenceTrackingResponse> SearchCustomersAsync(SearchReferenceRequest request,  Guid userId, CancellationToken ctx)
    {
        var customerResults =  await _customerRepository.SearchCustomersAsync(request.SearchTerm, request.CustomerNextCursor, request.PageSize, userId, ctx);

        var temp =  new ReferenceTrackingResponse
        {
            References = customerResults.Customers.Select(c => new ReferenceResponse
            {
                Id = c.Id,
                DisplayName = $"{c.CustomerReferenceNumber} | " + 
                              (c.UseBusinessName && !string.IsNullOrEmpty(c.BusinessName) ? c.BusinessName : 
                                  (!string.IsNullOrEmpty(c.FullName) ? c.FullName : c.Alias)),
                PhoneNumber = c.PhoneNumbers?.FirstOrDefault()?.PhoneNumber, // Get the first phone number or null
                ReferenceType = ReferenceType.Customer
            }).ToList(), // Ensure the Select results get stored into a list
            CustomerNextCursor = customerResults.NextCursor,
            CustomerHasNextPage = customerResults.HasNextPage
        };
        return temp;
    }

    public async Task<AddNewExternalReferenceResponse> AddNewExternalReferenceAsync(UserContext userContext, AddNewExternalReferenceRequest request, CancellationToken ctx)
    {
        var externalReferenceEntity = request.ToExternalReferenceEntity(userContext.UserId);
        
        var (id, name) = await _externalReferenceRepository.AddNewExternalReferenceAsync(externalReferenceEntity, ctx);

        // Create a new response object using the Id and Name from the tuple
        var response = new AddNewExternalReferenceResponse
        {
            Id = id,
            Name = name
        };
        
        return response;
    }
}