using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;

public class AddNewCustomerRequest
{
    public string? Title { get; set; }
    public string? Name { get; set; }
    public string? Surname { get; set; }
    public string? Alias { get; set; }
    public string CustomerType { get; set; }
    public string? CompanyName { get; set; }
    public bool UseCompanyName { get; set; }
    public List<EmailRequest>? Emails { get; set; }
    public List<PhoneNumberRequest>? PhoneNumbers { get; set; }
    public CustomerPlaceRequest? Property { get; set; }
    public bool IsBillingAddress { get; set; }
    public CustomerPlaceRequest? Billing{ get; set; }
    public List<string>? Tags { get; set; }
    public LinkReferenceRequest? Reference { get; set; }
    public string? Comment { get; set; }
}