namespace BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

public interface IEmailRequest
{
    public string EmailType { get; set; }
    public string Email { get; set; }
    public bool ReceiveNotifications { get; set; }
}