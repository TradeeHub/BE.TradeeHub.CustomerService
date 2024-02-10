using BE.TradeeHub.CustomerService.Application.Requests;
using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Application.Responses;

namespace BE.TradeeHub.CustomerService.Application.Interfaces;

public interface ICustomerService
{
    Task<AddNewCustomerResponse> AddNewCustomerAsync(UserContext userContext, AddNewCustomerRequest request, CancellationToken ctx);
    Task<AddNewExternalReferenceResponse> AddNewExternalReferenceAsync(UserContext userContext, AddNewExternalReferenceRequest request, CancellationToken ctx);
    Task<ReferenceTrackingResponse> SearchForPotentialReferencesAsync(SearchReferenceRequest request, Guid userId, CancellationToken ctx);
}