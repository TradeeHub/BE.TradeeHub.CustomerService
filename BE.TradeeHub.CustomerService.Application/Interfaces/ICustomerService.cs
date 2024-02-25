using BE.TradeeHub.CustomerService.Application.Responses;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Requests;
using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task<CustomerEntity> AddNewCustomerAsync(IUserContext userContext, AddNewCustomerRequest request, CancellationToken ctx);
    Task<AddNewExternalReferenceResponse> AddNewExternalReferenceAsync(IUserContext userContext, AddNewExternalReferenceRequest request, CancellationToken ctx);
    Task<ReferenceTrackingResponse> SearchForPotentialReferencesAsync(SearchReferenceRequest request, Guid userId, CancellationToken ctx);
    Task<CustomerEntity?> GetCustomerByIdAsync( Guid userId, ObjectId customerId, CancellationToken ctx);
}