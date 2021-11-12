using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public interface IMessageMapperService
    {
        void UpdateEntity(Message entity, CreateMessageCommand request);
        Expression<Func<Message, FileMessageResponse>> MapResponse { get; }
    }
}
