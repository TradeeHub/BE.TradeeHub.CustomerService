using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Enums;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Application.Extensions;

public static class AddNewCustomerRequestExtensions
{
    public static CustomerEntity ToCustomer(this AddNewCustomerRequest request, Guid userOwnerId, Guid createdBy)
    {
        var customerEntity = new CustomerEntity
        {
            UserOwnerId = userOwnerId,
            Title = request.Title,
            Name = request.Name,
            Surname = request.Surname,
            Alias = request.Alias,
            Status = CustomerStatus.Lead, // Default status for new customer
            Tags = request.Tags != null ? new HashSet<string>(request.Tags) : null,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy,
            Emails = request.Emails?.Select(e => new EmailEntity
            {
                Email = e.Email,
                EmailType = e.EmailType,
                ReceiveNotifications = e.ReceiveNotifications
            }).ToList(),
            PhoneNumbers = request.PhoneNumbers?.Select(p => new PhoneNumberEntity
            {
                PhoneNumber = p.PhoneNumber,
                PhoneNumberType = p.PhoneNumberType,
                ReceiveNotifications = p.ReceiveNotifications
            }).ToList(),
            // Initialize empty lists for later assignment
            Properties = new List<ObjectId>(),
            Comments = new List<ObjectId>()
        };

        return customerEntity;
    }

    public static PropertyEntity? ToProperty(this AddNewCustomerRequest request, Guid userOwnerId, Guid createdBy)
    {
        if (request.Property == null) return null;

        var propertyEntity = new PropertyEntity
        {
            UserOwnerId = userOwnerId,
            // Assuming PlaceEntity mapping is correctly handled
            Property = new PlaceEntity { /* Map from request.Property */ },
            Billing = request.IsBillingAddress && request.Billing != null ? new PlaceEntity { /* Map from request.Billing */ } : null,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = createdBy,
            Customers = new List<ObjectId>() // To be filled in after customer creation
        };

        return propertyEntity;
    }

    public static CommentEntity? ToComment(this AddNewCustomerRequest request, Guid userOwnerId, Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(request.Comment)) return null;

        var commentEntity = new CommentEntity
        {
            UserOwnerId = userOwnerId,
            Comment = request.Comment,
            CreatedAt = DateTime.UtcNow,
            CreatedById = createdBy,
            CommentType = CommentType.General,
            UploadUrls = new List<string>()
        };

        return commentEntity;
    }
}
