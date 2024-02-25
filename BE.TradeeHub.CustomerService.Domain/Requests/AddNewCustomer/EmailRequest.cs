using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

namespace BE.TradeeHub.CustomerService.Domain.Requests.AddNewCustomer;

public class EmailRequest : IEmailRequest
{
    public string EmailType { get; set; }
    public string Email { get; set; }
    public bool ReceiveNotifications { get; set; }
}