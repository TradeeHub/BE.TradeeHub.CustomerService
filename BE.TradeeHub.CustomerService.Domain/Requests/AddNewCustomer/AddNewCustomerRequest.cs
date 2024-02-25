using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class AddNewCustomerRequest : IAddNewCustomerRequest
{
    public string? Title { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Alias { get; set; }
    public string CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public bool UseCompanyName { get; set; }
    public IEnumerable<EmailRequest>? Emails { get; set; }
    public IEnumerable<PhoneNumberRequest>? PhoneNumbers { get; set; }
    public IEnumerable<PropertyRequest>? Properties { get; set; }
    public IEnumerable<string>? Tags { get; set; }
    public LinkReferenceRequest? Reference { get; set; }
    public string? Comment { get; set; }
}