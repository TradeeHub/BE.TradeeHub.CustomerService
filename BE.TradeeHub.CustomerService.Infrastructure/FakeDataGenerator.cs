using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using Bogus;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Infrastructure;

public abstract class FakeDataGenerator
{
    public static List<CustomerDbObject> CreateFakeCustomers(int quantity)
    {
        // Faker for EmailDbObject
        var emailFaker = new Faker<EmailDbObject>()
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.EmailType, f => f.Lorem.Word());

        // Faker for PhoneNumberDbObject
        var phoneFaker = new Faker<PhoneNumberDbObject>()
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.PhoneNumberType, f => f.Lorem.Word());
        
        // Faker for CustomerDbObject
        var customerFaker = new Faker<CustomerDbObject>()
            .RuleFor(c => c.Id, f => ObjectId.GenerateNewId())
            .RuleFor(c => c.Title, f => f.Name.Prefix())
            .RuleFor(c => c.Name, f => f.Name.FirstName())
            .RuleFor(c => c.Surname, f => f.Name.LastName())
            .RuleFor(c => c.Alias, f => f.Internet.UserName())
            .RuleFor(c => c.Emails, f => emailFaker.GenerateBetween(1, 3))
            .RuleFor(c => c.PhoneNumbers, f => phoneFaker.GenerateBetween(1, 3))
            // .RuleFor(c => c.Properties, f => new List<ObjectId> { ObjectId.GenerateNewId() }) // Simplified for the example
            .RuleFor(c => c.CreatedAt, f => f.Date.Past())
            .RuleFor(c => c.CreatedBy, f => Guid.NewGuid())
            .RuleFor(c => c.ModifiedAt, f => f.Date.Recent())
            .RuleFor(c => c.ModifiedBy, f => Guid.NewGuid())
            .RuleFor(c => c.ReferredByCustomer, f => ObjectId.GenerateNewId()) // Note: This can create deeply nested objects
            .RuleFor(c => c.ReferredByOther, f => f.Lorem.Sentence())
            .RuleFor(c => c.ReferralFeeFixed, f => f.Random.Decimal(1, 1000))
            .RuleFor(c => c.ReferralFeePercentage, f => f.Random.Decimal(1, 20))
            .RuleFor(c => c.CustomerRating, f => f.Random.Decimal(0, 5))
            .RuleFor(c => c.AdditionalNotes, f => f.Lorem.Paragraph());

        return customerFaker.Generate(quantity);
    }

    public static List<PropertyDbObject> CreateFakeProperties(int quantity)
    {
        // Faker for AddressDbObject
        var addressFaker = new Faker<AddressDbObject>()
            .RuleFor(a => a.Address, f => f.Address.StreetAddress())
            .RuleFor(a => a.City, f => f.Address.City())
            .RuleFor(a => a.Postcode, f => f.Address.ZipCode())
            .RuleFor(a => a.FullAddress, (f, a) => $"{a.Address}, {a.City}, {a.Postcode}");
        
        //Faker for PropertyDbObject
        var propertyFaker = new Faker<PropertyDbObject>()
            .RuleFor(p => p.Id, f => ObjectId.GenerateNewId())
            .RuleFor(p => p.Property, f => addressFaker.Generate())
            .RuleFor(p => p.Billing, f => f.Random.Bool() ? addressFaker.Generate() : null)
            .RuleFor(p => p.Location, f => f.Address.Country())
            .RuleFor(p => p.Customers, f => [ObjectId.GenerateNewId()]) // Simplified for the example
            .RuleFor(p => p.Quotes, f => new List<ObjectId> { ObjectId.GenerateNewId() }) // Similarly simplified
            .RuleFor(p => p.Jobs, f => new List<ObjectId> { ObjectId.GenerateNewId() }) // Similarly simplified
            .RuleFor(p => p.CreatedAt, f => f.Date.Past())
            .RuleFor(p => p.CreatedBy, f => Guid.NewGuid())
            .RuleFor(p => p.ModifiedAt, f => f.Date.Recent())
            .RuleFor(p => p.ModifiedBy, f => Guid.NewGuid());
        
        return propertyFaker.Generate(quantity);
    }
}