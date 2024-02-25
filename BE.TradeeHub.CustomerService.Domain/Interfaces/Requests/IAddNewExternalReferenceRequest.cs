using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IAddNewExternalReferenceRequest
{
    public string ReferenceType { get; }
    public string Name { get; }
    public string? CompanyName { get; }
    public bool UseCompanyName { get; }
    public PhoneNumberRequest? PhoneNumber { get; }
    public EmailRequest? Email { get; }
    public string? Url { get; }
    public PlaceRequest? Place { get; }
    public string? Description { get; }
    public CompensationDetailsRequest? Compensation { get; }
}