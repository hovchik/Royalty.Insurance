using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Messages
{
    public class ForwardMessageHandler :  IRequestHandler<ForwardMessageCommand, FileMessageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IRequestHandler<GetMessageByIdQuery, FileMessageResponse> _handler;
        private readonly IMessageMapperService _mapper;

        public ForwardMessageHandler(IApplicationDbContext context, IRequestHandler<GetMessageByIdQuery, FileMessageResponse> handler, IMessageMapperService mapper)
        {
            _context = context;
            _handler = handler;
            _mapper = mapper;
        }

        public async Task<FileMessageResponse> Handle(ForwardMessageCommand request, CancellationToken cancellationToken)
        {
            var messageAttachments = await _context.MessageAttachments
                .Where(item => item.MessageId.Equals(request.ParentId))
                .ToListAsync(cancellationToken);
            var entity = await CreateMessage(request, cancellationToken);
            foreach (var messageAttachment in messageAttachments)
            {
                messageAttachment.Message = entity;
                await _context.MessageAttachments.AddAsync(messageAttachment, cancellationToken);
            }
            await _context.SaveChangesAsync(cancellationToken);

            return await _handler.Handle(new GetMessageByIdQuery { Id = entity.Id }, cancellationToken);
        }

        private async Task<Message> CreateMessage(ForwardMessageCommand request, CancellationToken cancellationToken)
        {
            Message entity = new Message();
            _mapper.UpdateEntity(entity, request);
            entity.ParentId = request.ParentId;
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
