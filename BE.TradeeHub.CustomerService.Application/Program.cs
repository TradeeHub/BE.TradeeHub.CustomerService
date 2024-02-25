using BE.TradeeHub.CustomerService.Application;
using BE.TradeeHub.CustomerService.Application.Extensions;
using BE.TradeeHub.CustomerService.Application.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Interfaces;
using BE.TradeeHub.CustomerService.Domain.Interfaces.Repositories;
using BE.TradeeHub.CustomerService.Infrastructure;
using BE.TradeeHub.CustomerService.Infrastructure.Extensions;
using BE.TradeeHub.CustomerService.Infrastructure.Repositories;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);
var appSettings = new AppSettings(builder.Configuration);

builder.Services.AddSingleton<IAppSettings>(appSettings);
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserContext>();

builder.Services.AddSingleton<MongoDbContext>();
builder.Services.AddSingleton<IMongoDbContext, MongoDbContext>();
builder.Services.AddMongoDbCollections();

builder.Services.AddScoped<IExternalReferenceRepository, ExternalReferenceRepository>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IPropertyRepository, PropertyRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddAwsServices(builder.Configuration, appSettings);
builder.Services.AddCors(appSettings).AddAuth(appSettings);

builder.Services
    .AddGraphQLServer()
    .InitializeOnStartup()
    .AddGlobalObjectIdentification()
    .AddAuthorization()
    .AddTypes()
    .BindRuntimeType<ObjectId, IdType>()
    .AddTypeConverter<ObjectId, string>(o => o.ToString())
    .AddTypeConverter<string, ObjectId>(o => ObjectId.Parse(o))
    .AddType<UploadType>()
    .AddMongoDbSorting()
    .AddMongoDbProjections()
    .AddMongoDbPagingProviders()
    .AddMongoDbFiltering();

var app = builder.Build();

app.ExportGraphQlSchemaToFile();

var dbContext = app.Services.GetRequiredService<IMongoDbContext>();
dbContext.EnsureIndexesCreated();

app.UseCors("GraphQLCorsPolicy"); // Apply the CORS policy
app.UseAuthentication();
app.UseAuthorization();
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