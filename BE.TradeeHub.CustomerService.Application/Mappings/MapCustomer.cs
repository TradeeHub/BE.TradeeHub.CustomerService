using BE.TradeeHub.CustomerService.Application.Requests.AddNewCustomer;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Entities.Reference;

namespace BE.TradeeHub.CustomerService.Application.Mappings;

public static class AddNewCustomerRequestExtensions
{
    public static CustomerEntity ToCustomerEntity(this AddNewCustomerRequest request, Guid userOwnerId, Guid createdBy)
    {
        var emails = request.Emails?.Select(e => new EmailEntity(e.Email, e.EmailType, e.ReceiveNotifications)) ??
                     Enumerable.Empty<EmailEntity>();
        var phoneNumbers =
            request.PhoneNumbers?.Select(p =>
                new PhoneNumberEntity(p.PhoneNumber, p.PhoneNumberType, p.ReceiveNotifications)) ??
            Enumerable.Empty<PhoneNumberEntity>();

        var customerEntity = new CustomerEntity(
            userOwnerId: userOwnerId,
            title: request.Title,
            name: request.Name,
            surname: request.Surname,
            alias: request.Alias,
            tags: request.Tags,
            createdBy: createdBy,
            emails: emails,
            phoneNumbers: phoneNumbers
        );

        return customerEntity;
    }

    public static PropertyEntity? ToPropertyEntity(this AddNewCustomerRequest request, Guid userOwnerId, Guid createdBy)
    {
        if (request.Property == null) return null;

        var propertyEntity = new PropertyEntity(
            userOwnerId: userOwnerId,
            property: request.Property.ToPlaceEntity(),
            billing: request.Billing?.ToPlaceEntity(),
            createdBy: createdBy,
            request.IsBillingAddress
        );

        return propertyEntity;
    }

    public static CommentEntity? ToCommentEntity(this AddNewCustomerRequest request, Guid userOwnerId, Guid createdBy)
    {
        if (string.IsNullOrWhiteSpace(request.Comment)) return null;

        var commentEntity = new CommentEntity(userOwnerId, request.Comment, createdBy);

        return commentEntity;
    }

    private static PlaceEntity ToPlaceEntity(this CustomerPlaceRequest request)
    {
        // Validation or defaulting logic could be added here if necessary

        var placeEntity = new PlaceEntity
        {
            PlaceId = request.PlaceId,
            Address = request.Address,
            Country = request.Country,
            CountryCode = request.CountryCode,
            CallingCode = request.CallingCode,
            Location = new LocationEntity
            {
                Lat = request.Location.Lat,
                Lng = request.Location.Lng
            },
            Viewport = new ViewportEntity
            {
                Northeast = new LocationEntity
                {
                    Lat = request.Viewport.Northeast.Lat,
                    Lng = request.Viewport.Northeast.Lng
                },
                Southwest = new LocationEntity
                {
                    Lat = request.Viewport.Southwest.Lat,
                    Lng = request.Viewport.Southwest.Lng
                }
            }
        };

        return placeEntity;
    }

    public static ExternalReferenceEntity ToExternalReferenceEntity(this AddNewExternalReferenceRequest request,
        Guid userOwnerId)
    {
        return new ExternalReferenceEntity
        {
            UserOwnerId = userOwnerId,
            ReferenceType = request.ReferenceType,
            Name = request.Name,
            CompanyName = request.CompanyName,
            PhoneNumber = request.PhoneNumber != null
                ? new PhoneNumberEntity(request.PhoneNumber.PhoneNumber, request.PhoneNumber.PhoneNumberType,
                    request.PhoneNumber.ReceiveNotifications)
                : null,
            Email = request.Email != null
                ? new EmailEntity(request.Email.Email, request.Email.EmailType, request.Email.ReceiveNotifications)
                : null,
            Url = request.Url,
            Place = request.Place?.ToPlaceEntity(),
            Description = request.Description,
            Compensation = request.Compensation != null
                ? new CompensationDetailsEntity
                {
                    Amount = request.Compensation.Amount,
                    Currency = request.Compensation.Currency,
                    CompensationType = request.Compensation.Type
                }
                : null
        };
    }
}