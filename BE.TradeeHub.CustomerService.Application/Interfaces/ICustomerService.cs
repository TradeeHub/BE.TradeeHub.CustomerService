using BE.TradeeHub.CustomerService.Application.Requests;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;
using BE.TradeeHub.CustomerService.Domain.Entities;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task<AddNewCustomerResponse> AddNewCustomerAsync(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx);
    Task<AddNewExternalReferenceResponse> AddNewExternalReferenceAsync(UserContext userContext, AddNewExternalReferenceRequest request, CancellationToken ctx);
    Task<ReferenceTrackingResponse> SearchForPotentialReferencesAsync(SearchReferenceRequest request, Guid userId, CancellationToken ctx);
    Task<CustomerEntity?> GetCustomerByIdAsync( Guid userId, ObjectId customerId, CancellationToken ctx);
}