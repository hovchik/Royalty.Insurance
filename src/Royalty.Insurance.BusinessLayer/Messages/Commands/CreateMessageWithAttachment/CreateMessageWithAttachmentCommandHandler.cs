using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class CreateMessageWithAttachmentCommandHandler : IRequestHandler<CreateMessageWithAttachmentCommand, FileMessageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IRequestHandler<GetMessageByIdQuery, FileMessageResponse> _handler;
        private readonly IMessageMapperService _mapper;

        public CreateMessageWithAttachmentCommandHandler(IMessageMapperService mapper, IApplicationDbContext context, IRequestHandler<GetMessageByIdQuery, FileMessageResponse> handler)
        {
            _mapper = mapper;
            _context = context;
            _handler = handler;
        }

        public async Task<FileMessageResponse> Handle(CreateMessageWithAttachmentCommand request, CancellationToken cancellationToken)
        {
            var entity = await CreateMessage(request, cancellationToken);
            foreach (var requestFile in request.Files)
            {
                Attachment attachment = new Attachment { Name = requestFile.FileName };
                attachment.MessageAttachments.Add(new MessageAttachment
                {
                    Message = entity,
                    Attachment = attachment
                });
                await _context.Attachments.AddAsync(attachment, cancellationToken);
            }

            await _context.SaveChangesAsync(cancellationToken);

            return await _handler.Handle(new GetMessageByIdQuery { Id = entity.Id }, cancellationToken);
        }

        private async Task<Message> CreateMessage(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            Message entity = new Message();
            _mapper.UpdateEntity(entity, request);
            await _context.Messages.AddAsync(entity, cancellationToken);
            await SetMessageUnReadUsers(request.UserId, entity, request.GroupId, cancellationToken);

            return entity;
        }

        private async Task SetMessageUnReadUsers(int userId, Message message, int groupId, CancellationToken cancellationToken)
        {
            var members = await _context.GroupMembers
                .Where(member => !member.MemberId.Equals(userId) && member.GroupId.Equals(groupId))
                .ToListAsync(cancellationToken);
            foreach (var member in members)
            {
                var entity = new UnreadMessage()
                {
                    Message = message,
                    SendUserId = userId,
                    ReadUserId = member.MemberId,
                    GroupId = groupId,
                };
                await _context.UnreadMessages.AddAsync(entity, cancellationToken);
            }
        }
    }
}
