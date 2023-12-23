    using BE.TradeeHub.CustomerService.Application;
    using BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;
    using BE.TradeeHub.CustomerService.Application.GraphQL.Queries;
    using BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;
    using BE.TradeeHub.CustomerService.Application.GraphQL.Types;
    using BE.TradeeHub.CustomerService.Infrastructure;
    using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
    using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
    using MongoDB.Bson;
    using MongoDB.Driver;

    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();
    builder.Services.AddSingleton<MongoDbContext>();
    builder.Services.AddScoped<CustomerRepository>();
    builder.Services.AddScoped<PropertyRepository>();
    builder.Services.AddScoped<CustomerService>();
    builder.Services.AddScoped<TypeResolver>();
    builder.Services.AddScoped<CustomerPropertiesDataLoader>();
    // builder.Services.AddScoped<AssetNode>();


    builder.Services.AddSingleton<IMongoCollection<CustomerDbObject>>(serviceProvider =>
    {
        var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
        return mongoDbContext.Customers; // Assuming this is the property name in MongoDbContext for the collection
    });

    builder.Services.AddSingleton<IMongoCollection<PropertyDbObject>>(serviceProvider =>
    {
        var mongoDbContext = serviceProvider.GetRequiredService<MongoDbContext>();
        return mongoDbContext.Properties; // Assuming this is the property name in MongoDbContext for the collection
    });

    builder.Services
        .AddGraphQLServer()
        .AddDataLoader<CustomerPropertiesDataLoader>()
        .BindRuntimeType<ObjectId, IdType>()
        .AddTypeConverter<ObjectId, string>(o => o.ToString())
        .AddTypeConverter<string, ObjectId>(o => ObjectId.Parse(o))
        .AddQueryType<Query>()
        .AddType<CustomerType>()
        .AddType<PropertyType>()
        .AddMongoDbSorting()
        .AddMongoDbProjections()
        .AddMongoDbPagingProviders()
        .AddMongoDbFiltering();

    var app = builder.Build();

    app.UseHttpsRedirection();
    app.UseRouting();
    app.MapGraphQL();

    app.Run();