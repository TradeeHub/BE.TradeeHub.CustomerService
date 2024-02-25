using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Enums;
using Bogus;
using MongoDB.Bson;

namespace BE.TradeeHub.CustomerService.Infrastructure;

public abstract class FakeDataGenerator
{
    private static readonly HashSet<string> generatedCRNs = new HashSet<string>();

    public static List<CustomerEntity> CreateFakeCustomers(int quantity)
    {
        // Faker for EmailDbObject
        var emailFaker = new Faker<EmailEntity>()
            .RuleFor(e => e.Email, f => f.Internet.Email())
            .RuleFor(e => e.EmailType, f => f.Lorem.Word());

        // Faker for PhoneNumberDbObject
        var phoneFaker = new Faker<PhoneNumberEntity>()
            .RuleFor(p => p.PhoneNumber, f => f.Phone.PhoneNumber())
            .RuleFor(p => p.PhoneNumberType, f => f.Lorem.Word());
        
        // Faker for CustomerDbObject
        // var customerFaker = new Faker<CustomerDbObject>()
        //     .RuleFor(c => c.Id, f => ObjectId.GenerateNewId())
        //     .RuleFor(c => c.Status,
        //         f => f.PickRandom(Enum.GetNames(typeof(CustomerStatus)))
        //             .Replace("_", " ")) // Pick random status and replace underscores with spaces
        //     .RuleFor(c => c.CustomerReferenceNumber, f => GenerateUniqueCustomerReferenceNumber(f))
        //     .RuleFor(c => c.Tags, f => new HashSet<string>(f.Lorem.Words(5)))
        //     .RuleFor(c => c.Comments, f => GenerateFakeComments(f, 3))
        //     .RuleFor(c => c.Title, f => f.Name.Prefix())
        //     .RuleFor(c => c.Name, f => f.Name.FirstName())
        //     .RuleFor(c => c.Surname, f => f.Name.LastName())
        //     .RuleFor(c => c.Alias, f => f.Internet.UserName())
        //     .RuleFor(c => c.Emails, f => emailFaker.GenerateBetween(1, 3))
        //     .RuleFor(c => c.PhoneNumbers, f => phoneFaker.GenerateBetween(1, 3))
        //     .RuleFor(c => c.CreatedAt, f => f.Date.Past())
        //     .RuleFor(c => c.CreatedBy, f => Guid.NewGuid())
        //     .RuleFor(c => c.ModifiedAt, f => f.Date.Recent())
        //     .RuleFor(c => c.ModifiedBy, f => Guid.NewGuid())
        //     .RuleFor(c => c.ReferredByCustomer,
        //         f => ObjectId.GenerateNewId()) // Note: This can create deeply nested objects
        //     .RuleFor(c => c.ReferredByOther, f => f.Lorem.Sentence())
        //     .RuleFor(c => c.ReferralFeeFixed, f => f.Random.Decimal(1, 1000))
        //     .RuleFor(c => c.ReferralFeePercentage, f => f.Random.Decimal(1, 20))
        //     .RuleFor(c => c.CustomerRating, f => f.Random.Decimal(0, 5));

        // return customerFaker.Generate(quantity);
        return null;
    }

    private static string GenerateUniqueCustomerReferenceNumber(Faker faker)
    {
        string crn;
        do
        {
            crn = "#CRN-" + faker.Random.Number(1, 99999).ToString();
        } while (generatedCRNs.Contains(crn));
        generatedCRNs.Add(crn);
        return crn;
    }
    
    // private static List<CommentEntity> GenerateFakeComments(Faker faker, int maxComments)
    // {
    //     // var commentFaker = new Faker<CommentEntity>()
    //     //     .RuleFor(c => c.Comment, f => f.Lorem.Sentence())
    //     //     .RuleFor(c => c.UploadUrls, f => f.Make(f.Random.Int(1, 5), () => f.Image.PicsumUrl()));
    //     //
    //     // return commentFaker.Generate(faker.Random.Int(1, maxComments));
    // }
    public static List<PropertyEntity> CreateFakeProperties(int quantity)
    {
        // // Faker for AddressDbObject
        // var addressFaker = new Faker<AddressDbObject>()
        //     .RuleFor(a => a.Address, f => f.Address.StreetAddress())
        //     .RuleFor(a => a.City, f => f.Address.City())
        //     .RuleFor(a => a.Postcode, f => f.Address.ZipCode())
        //     .RuleFor(a => a.FullAddress, (f, a) => $"{a.Address}, {a.City}, {a.Postcode}");
        //
        // //Faker for PropertyDbObject
        // var propertyFaker = new Faker<PropertyDbObject>()
        //     .RuleFor(p => p.Id, f => ObjectId.GenerateNewId())
        //     .RuleFor(p => p.Property, f => addressFaker.Generate())
        //     .RuleFor(p => p.Billing, f => f.Random.Bool() ? addressFaker.Generate() : null)
        //     .RuleFor(p => p.Location, f => f.Address.Country())
        //     .RuleFor(p => p.Customers, f => [ObjectId.GenerateNewId()]) // Simplified for the example
        //     .RuleFor(p => p.Quotes, f => new List<ObjectId> { ObjectId.GenerateNewId() }) // Similarly simplified
        //     .RuleFor(p => p.Jobs, f => new List<ObjectId> { ObjectId.GenerateNewId() }) // Similarly simplified
        //     .RuleFor(p => p.CreatedAt, f => f.Date.Past())
        //     .RuleFor(p => p.CreatedBy, f => Guid.NewGuid())
        //     .RuleFor(p => p.ModifiedAt, f => f.Date.Recent())
        //     .RuleFor(p => p.ModifiedBy, f => Guid.NewGuid());
        //
        // return propertyFaker.Generate(quantity);
        return null;
    }
}