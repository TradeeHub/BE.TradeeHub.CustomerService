using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Application.Responses;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;
using BE.TradeeHub.CustomerService.Domain.Enums;
using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Domain.Requests;
using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application;

public class CustomerService : ICustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IExternalReferenceRepository _externalReferenceRepository;

    public CustomerService(ICustomerRepository customerRepository,
        IExternalReferenceRepository externalReferenceRepository)
    {
        _externalReferenceRepository = externalReferenceRepository;
        _customerRepository = customerRepository;
    }

    public async Task<CustomerEntity> AddNewCustomerAsync(IUserContext userContext,
        AddNewCustomerRequest request,
        CancellationToken ctx)
    {
        var customerEntity = new CustomerEntity(request, userContext);
                
        var propertyEntities = request.Properties?.Select(p => new PropertyEntity(p, userContext)).ToList() ?? [];

        var commentEntity = !string.IsNullOrEmpty(request.Comment) ? new CommentEntity(request.Comment,  userContext) : null;

        var comments = commentEntity != null ? [commentEntity] : new List<CommentEntity>();

        // Pass the entities to the repository method, which now receives empty lists if entities are null
        var (id, customerReferenceNumber) =
            await _customerRepository.AddNewCustomerAsync(customerEntity, propertyEntities, comments, ctx);

        return new CustomerEntity() { Id = id, CustomerReferenceNumber = customerReferenceNumber };
    }

    public async Task<ReferenceTrackingResponse> SearchForPotentialReferencesAsync(SearchReferenceRequest request,
        Guid userId, CancellationToken ctx)
    {
        // Divide the page size equally between external references and customers
        var halfPageSize = request.PageSize / 2;
        var customerPageSize = halfPageSize;

        // Search for external references
        var externalResults = await _externalReferenceRepository.SearchExternalReferencesAsync(request.SearchTerm,
            request.ExternalNextCursor, halfPageSize, userId, ctx);

        // Calculate the remaining page size for customers if external results are fewer than allocated
        if (externalResults.ExternalReferences != null && externalResults.ExternalReferences.Count < halfPageSize)
        {
            customerPageSize += halfPageSize - externalResults.ExternalReferences.Count;
        }

        // Search for customers with the possibly adjusted page size
        var customerResults = await _customerRepository.SearchCustomersAsync(request.SearchTerm,
            request.CustomerNextCursor, customerPageSize, userId, ctx);

        // Combine results
        var references = new List<ReferenceResponse>();

        // Add external references to the response
        references.AddRange(externalResults.ExternalReferences.Select(e => new ReferenceResponse
        {
            Id = e.Id,
            DisplayName = $"External | " +
                          (e.UseCompanyName && !string.IsNullOrEmpty(e.CompanyName) ? e.CompanyName : e.Name),
            PhoneNumber = e.PhoneNumber?.PhoneNumber,
            ReferenceType = ReferenceType.External
        }));

        // Add customer references to the response
        references.AddRange(customerResults.Customers.Select(c => new ReferenceResponse
        {
            Id = c.Id,
            DisplayName = $"{c.CustomerReferenceNumber} | " +
                          (c.UseCompanyName && !string.IsNullOrEmpty(c.CompanyName) ? c.CompanyName :
                              !string.IsNullOrEmpty(c.FullName) ? c.FullName : c.Alias),
            PhoneNumber = c.PhoneNumbers?.FirstOrDefault()?.PhoneNumber, // Get the first phone number or null
            ReferenceType = ReferenceType.Customer
        }));

        // Prepare the final response
        var response = new ReferenceTrackingResponse
        {
            References = references,
            CustomerNextCursor = customerResults.HasNextPage ? customerResults.NextCursor : null,
            ExternalNextCursor = externalResults.HasNextPage ? externalResults.NextCursor : null,
            CustomerHasNextPage = customerResults.HasNextPage,
            ExternalHasNextPage = externalResults.HasNextPage
        };

        return response;
    }

    public async Task<AddNewExternalReferenceResponse> AddNewExternalReferenceAsync(IUserContext userContext,
        AddNewExternalReferenceRequest request, CancellationToken ctx)
    {
        var externalReferenceEntity = new ExternalReferenceEntity(request, userContext.UserId);

        var (id, name) = await _externalReferenceRepository.AddNewExternalReferenceAsync(externalReferenceEntity, ctx);

        // Create a new response object using the Id and Name from the tuple
        var response = new AddNewExternalReferenceResponse
        {
            Id = id,
            Name = name
        };

        return response;
    }

    public async Task<CustomerEntity?> GetCustomerByIdAsync(Guid userId, ObjectId customerId, CancellationToken ctx)
    {
        return await _customerRepository.GetCustomerByIdAsync(userId, customerId, ctx);
    }
}