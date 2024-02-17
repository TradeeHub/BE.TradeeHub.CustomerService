using System.Text.RegularExpressions;
using BE.TradeeHub.CustomerService.Domain.Entities;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Domain.Results;
using MongoDB.Bson;
using MongoDB.Driver;

namespace BE.TradeeHub.CustomerService.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly MongoDbContext _dbContext;

    public CustomerRepository(MongoDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<CustomerEntity?> GetCustomerByIdAsync(Guid userId, ObjectId customerId, CancellationToken ctx)
    {
        var userOwnerFilter = Builders<CustomerEntity>.Filter.Eq(c => c.UserOwnerId, userId);
        var customerIdFilter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, customerId);
        var combinedFilter = Builders<CustomerEntity>.Filter.And(userOwnerFilter, customerIdFilter);

        var customer = await _dbContext.Customers.Find(combinedFilter).FirstOrDefaultAsync(ctx);
        return customer;
    }

    public async Task<CustomerEntity?> GetCustomerByIdAsync(ObjectId customerId, CancellationToken ctx)
    {
        var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Id, customerId);
        var customer = await _dbContext.Customers.Find(filter).FirstOrDefaultAsync(ctx);
        return customer;
    }

    public async Task<IEnumerable<CustomerEntity>> GetAllCustomersAsync()
    {
        return await _dbContext.Customers.Find(_ => true).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByPropertyIdAsync(ObjectId propertyId)
    {
        var filter = Builders<CustomerEntity>.Filter.AnyEq(c => c.Properties, propertyId);
        return await _dbContext.Customers.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByNameAsync(string name)
    {
        var filter = Builders<CustomerEntity>.Filter.Eq(c => c.Name, name);
        return await _dbContext.Customers.Find(filter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByAddressAsync(string address)
    {
        // Find properties with the given address
        var propertyFilter = Builders<PropertyEntity>.Filter.Eq(p => p.Property.Address, address);
        var properties = await _dbContext.Properties.Find(propertyFilter).ToListAsync();

        // Extract property IDs
        var propertyIds = properties.Select(p => p.Id);

        // Find customers linked to these properties
        var customerFilter = Builders<CustomerEntity>.Filter.AnyIn(c => c.Properties, propertyIds);
        return await _dbContext.Customers.Find(customerFilter).ToListAsync();
    }

    public async Task<IEnumerable<CustomerEntity>> GetCustomersByIdsAsync(IEnumerable<ObjectId> customerIds)
    {
        // Build a filter to select customers with IDs that are in the customerIds list
        var filter = Builders<CustomerEntity>.Filter.In(c => c.Id, customerIds);

        // Execute the query using the Customers collection from MongoDbContext
        var cursor = await _dbContext.Customers.FindAsync(filter);

        return await cursor.ToListAsync();
    }

    public async Task<(ObjectId Id, string CustomerReferenceNumber)> AddNewCustomerAsync(CustomerEntity customer,
        IList<PropertyEntity> properties,
        IList<CommentEntity> comments, CancellationToken ctx)
    {
        using var session = await _dbContext.Client.StartSessionAsync(cancellationToken: ctx);

        session.StartTransaction();

        try
        {
            // Generate unique CRN for the customer
            customer.CustomerReferenceNumber =
                await GenerateUniqueCustomerReferenceNumber(customer.UserOwnerId, session, ctx);

            // Initialize Properties and Comments lists if necessary
            customer.Properties = new List<ObjectId>();
            customer.Comments = new List<ObjectId>();

            // Add customer first to get its ID
            await _dbContext.Customers.InsertOneAsync(session, customer, cancellationToken: ctx);

            // Handle Properties in a single operation if there are any
            if (properties.Any())
            {
                foreach (var property in properties)
                {
                    property.Customers = [customer.Id];
                }

                await _dbContext.Properties.InsertManyAsync(session, properties, cancellationToken: ctx);
                customer.Properties.AddRange(properties.Select(p => p.Id));
            }

            // Handle Comments in a single operation if there are any, making sure to link them to the customer
            if (comments.Any())
            {
                foreach (var comment in comments)
                {
                    comment.CustomerId = customer.Id;
                }

                await _dbContext.Comments.InsertManyAsync(session, comments, cancellationToken: ctx);
                customer.Comments.AddRange(comments.Select(c => c.Id));
            }

            // Update the customer with the new lists of property and comment IDs if any were added
            if (customer.Properties?.Any() == true || customer.Comments?.Any() == true)
            {
                var updateDefinition = Builders<CustomerEntity>.Update
                    .Set(c => c.Properties, customer.Properties)
                    .Set(c => c.Comments, customer.Comments);
                await _dbContext.Customers.UpdateOneAsync(session, c => c.Id == customer.Id, updateDefinition,
                    cancellationToken: ctx);
            }

            await session.CommitTransactionAsync(ctx);

            return (customer.Id, customer.CustomerReferenceNumber);
        }
        catch (Exception ex)
        {
            await session.AbortTransactionAsync(ctx);
            throw;
        }
    }


    private async Task<string> GenerateUniqueCustomerReferenceNumber(Guid userOwnerId, IClientSessionHandle session,
        CancellationToken ctx)
    {
        var customerRefNumCollection = _dbContext.CustomerReferenceNumber;

        // Define the filter to find the document for the specific UserOwnerId
        var filter = Builders<CustomerReferenceNumberEntity>.Filter.Eq(doc => doc.UserOwnerId, userOwnerId);

        // Define the update operation to increment the counter
        var update = Builders<CustomerReferenceNumberEntity>.Update.Inc(doc => doc.Counter, 1);

        // Specify that the operation should return the document after the update and create a new document if one doesn't exist
        var options = new FindOneAndUpdateOptions<CustomerReferenceNumberEntity, CustomerReferenceNumberEntity>
        {
            ReturnDocument = ReturnDocument.After,
            IsUpsert = true
        };

        // Perform the find and update operation atomically within the session
        var result = await customerRefNumCollection.FindOneAndUpdateAsync(session, filter, update, options, ctx);

        // If the result is null, explicitly insert a new document with Counter set to 1.
        // This case might occur if the FindOneAndUpdateAsync doesn't automatically upsert in certain conditions.
        if (result == null)
        {
            var newEntry = new CustomerReferenceNumberEntity
            {
                UserOwnerId = userOwnerId,
                Counter = 1
            };
            await customerRefNumCollection.InsertOneAsync(session, newEntry, cancellationToken: ctx);
            return "CRN-1";
        }

        // Construct the CRN using the updated counter value
        return $"CRN-{result.Counter}";
    }

    public async Task<CustomerPageResult> SearchCustomersAsync(string searchTerm, string? lastCursor, int pageSize,
        Guid userId, CancellationToken ctx)
    {
        try
        {
            var objectIdLastCursor = string.IsNullOrEmpty(lastCursor) ? (ObjectId?)null : new ObjectId(lastCursor);

            // Define the lookup stage to join with the Properties collection
            var lookupStage = new BsonDocument("$lookup", new BsonDocument
            {
                { "from", "Properties" },
                { "localField", "Properties" },
                { "foreignField", "_id" },
                { "as", "PropertyDetails" }
            });

            // Define the match stage with extended search criteria including property details
            var matchCriteria = new BsonDocument("$and", new BsonArray
            {
                new BsonDocument("UserOwnerId", userId),
                new BsonDocument("$or", new BsonArray
                {
                    new BsonDocument("CustomerReferenceNumber", new BsonRegularExpression(searchTerm, "i")),
                    new BsonDocument("FullName", new BsonRegularExpression(searchTerm, "i")),
                    new BsonDocument("Emails.Email", new BsonRegularExpression(searchTerm, "i")),
                    new BsonDocument("PhoneNumbers.PhoneNumber", new BsonRegularExpression(searchTerm, "i")),
                    new BsonDocument("PropertyDetails.Property.Address", new BsonRegularExpression(searchTerm, "i"))
                })
            });

            // Apply cursor-based pagination for properties
            if (objectIdLastCursor != null)
            {
                matchCriteria["$and"].AsBsonArray
                    .Add(new BsonDocument("_id", new BsonDocument("$gt", objectIdLastCursor)));
            }

            var matchStage = new BsonDocument("$match", matchCriteria);

            // Project stage to exclude PropertyDetails from the output if not needed
            var projectStage = new BsonDocument("$project", new BsonDocument("PropertyDetails", 0));

            var pipeline = new[]
            {
                lookupStage,
                matchStage,
                projectStage, // Include this stage if you want to exclude property details from final output
                new BsonDocument("$sort", new BsonDocument("_id", 1)), // Sort by ID for pagination
                new BsonDocument("$limit", pageSize + 1) // Fetch one extra record to check for next page
            };

            var customers = await _dbContext.Customers.Aggregate<CustomerEntity>(pipeline, cancellationToken: ctx)
                .ToListAsync(ctx);

            // Pagination logic
            var hasNextPage = customers.Count > pageSize;
            var returnedCustomers = hasNextPage ? customers.Take(pageSize).ToList() : customers;
            var nextCursor = hasNextPage ? returnedCustomers.Last().Id.ToString() : null;

            return new CustomerPageResult
            {
                Customers = returnedCustomers,
                HasNextPage = hasNextPage,
                NextCursor = nextCursor
            };
        }
        catch (Exception ex)
        {
            // Consider logging the exception
            return new CustomerPageResult
            {
                Customers = new List<CustomerEntity>(),
                HasNextPage = false,
                NextCursor = null
            };
        }
    }
}