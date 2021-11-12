using System;
using System.Linq;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class MessageMapperService : IMessageMapperService
    {
        public void UpdateEntity(Message entity, CreateMessageCommand request)
        {
            entity.Body = request.Content;
            entity.RecipientGroupId = request.GroupId;
            entity.SenderId = request.UserId;
        }

        public Expression<Func<Message, FileMessageResponse>> MapResponse
        {
            get
            {
                return entity => new FileMessageResponse
                {
                    Content = entity.Body,
                    UserId = entity.SenderId,
                    GroupCreatedById = entity.RecipientGroup.CreatedBy,
                    GroupTypeId =  entity.RecipientGroup.GroupTypeId,
                    GroupId = entity.RecipientGroupId,
                    SentDate = entity.CreateDatetimeUtc,
                    MessageId = entity.Id,
                    IsRead = entity.UnreadMessages.Count == 0, //TODO: improve entity  to avoid performance issues
                    MessageAuthorId = entity.ParentId == null ? (int?)null : entity.Parent.SenderId,
                    AttachmentsPath = entity.MessageAttachments
                        .Select(item => item.Attachment.UserGarageId.HasValue ? 
                            (
                                item.Attachment.IsDeleted ? null : $"api/v1/me/files/user-file/{item.Attachment.Name}?id={item.Message.SenderId}"
                            ) : $"api/v1/fileUploads/{item.Attachment.Name}")// TODO: investigate to get route dynamic rather than cnst
                };
            }
        }
    }
}
