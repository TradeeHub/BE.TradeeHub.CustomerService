using System.Diagnostics;
using BE.TradeeHub.CustomerService.Application;
using BE.TradeeHub.CustomerService.Application.GraphQL.DataLoader;
using BE.TradeeHub.CustomerService.Application.GraphQL.Mutations;
using BE.TradeeHub.CustomerService.Application.GraphQL.Queries;
using BE.TradeeHub.CustomerService.Application.GraphQL.QueryResolvers;
using BE.TradeeHub.CustomerService.Application.GraphQL.Types;
using BE.TradeeHub.CustomerService.Infrastructure;
using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using HotChocolate.Execution;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("GraphQLCorsPolicy", builder =>
    {
        builder.WithOrigins("http://localhost:3000") // Replace with the client's URL
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

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
    .AddMutationType<Mutation>()
    .AddType<CustomerType>()
    .AddType<PropertyType>()
    .AddMongoDbSorting()
    .AddMongoDbProjections()
    .AddMongoDbPagingProviders()
    .AddMongoDbFiltering();

var app = builder.Build();

if(Debugger.IsAttached)
{
    var resolver = app.Services.GetService<IRequestExecutorResolver>();
    var executor = resolver?.GetRequestExecutorAsync().Result;
    if (executor != null)
    {
        const string schemaFile = "schema.graphql";
        var newSchema = executor.Schema.ToString();
        var oldSchema = File.ReadAllText(schemaFile);
        if (newSchema != oldSchema)
            File.WriteAllText(schemaFile, newSchema);
    }
}
//app.UseHttpsRedirection();
app.UseCors("GraphQLCorsPolicy"); // Apply the CORS policy

app.UseRouting();

app.MapGraphQL();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/graphql/", permanent: false);
    }
    else
    {
        await next();
    }
});

app.Run();