using BE.TradeeHub.CustomerService.Domain.Entities;

namespace BE.TradeeHub.CustomerService.Application.GraphQL.Types;

public class CommentType : ObjectType<CommentEntity>
{
    protected override void Configure(IObjectTypeDescriptor<CommentEntity> descriptor)
    {
    }
}