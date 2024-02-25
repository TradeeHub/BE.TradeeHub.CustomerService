using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;
using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Requests;

public class AddNewExternalReferenceRequest : IAddNewExternalReferenceRequest
{
    public string ReferenceType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? CompanyName { get; set; }
    public bool UseCompanyName { get; set; }
    public PhoneNumberRequest? PhoneNumber { get; set; }
    public EmailRequest? Email { get; set; }
    public string? Url { get; set; } = null!;
    public PlaceRequest? Place { get; set; }
    public string? Description { get; set; } = null!;
    public CompensationDetailsRequest? Compensation  { get; set; }
}