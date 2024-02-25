    using BE.TradeeHub.CustomerService.Domain.Interfaces.Requests;

    namespace BE.TradeeHub.CustomerService.Domain.Entities;

    public class EmailEntity
    {
        public string EmailType { get; set; } = null!;
        public string Email { get; set; } = null!;
        public bool ReceiveNotifications { get; set; }
        
        public EmailEntity()
        {
        }
        
        public EmailEntity(IEmailRequest addEmailRequest)
        {
            Email = addEmailRequest.Email.Trim();
            EmailType = addEmailRequest.EmailType.Trim();
            ReceiveNotifications = addEmailRequest.ReceiveNotifications;
        }
    }