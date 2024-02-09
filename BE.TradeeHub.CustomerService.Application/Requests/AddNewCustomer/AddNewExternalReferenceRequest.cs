namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class AddNewExternalReferenceRequest
{
    public string ReferenceType { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? CompanyName { get; set; }
    public PhoneNumberRequest? PhoneNumber { get; set; }
    public EmailRequest? Email { get; set; }
    public string? Url { get; set; } = null!;
    public CustomerPlaceRequest? Place { get; set; }
    public string? Description { get; set; } = null!;
    public CompensationDetailsRequest? Compensation  { get; set; }
}