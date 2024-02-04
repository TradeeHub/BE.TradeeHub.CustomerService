using BE.TradeeHub.CustomerService.Infrastructure.DbObjects;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class CommentType : ObjectType<CommentDbObject>
{
    protected override void Configure(IObjectTypeDescriptor<CommentDbObject> descriptor)
    {
    }
}