using BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IAddNewCustomerRequest
{
    public string? Title { get; }
    public string? Name { get; }
    public string? Surname { get; }
    public string? Alias { get; }
    public string CustomerType { get; }
    public string? CompanyName { get; }
    public bool UseCompanyName { get; }
    public IEnumerable<EmailRequest>? Emails { get; }
    public IEnumerable<PhoneNumberRequest>? PhoneNumbers { get; }
    public IEnumerable<PropertyRequest>? Properties { get; }
    public IEnumerable<string>? Tags { get; }
    public LinkReferenceRequest? Reference { get; }
    public string? Comment { get; }
}