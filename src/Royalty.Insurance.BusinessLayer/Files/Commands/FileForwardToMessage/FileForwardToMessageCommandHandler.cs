using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Messages;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Files.Queries.GetFileById
{
    public class FileForwardToMessageCommandHandler : IRequestHandler<FileForwardToMessageCommand, FileMessageResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IRequestHandler<GetMessageByIdQuery, FileMessageResponse> _handler;

        public FileForwardToMessageCommandHandler(IApplicationDbContext context, IUserGarageMapperService mapper, IRequestHandler<GetMessageByIdQuery, FileMessageResponse> handler)
        {
            _context = context;
            _handler = handler;
        }

        public async Task<FileMessageResponse> Handle(FileForwardToMessageCommand request, CancellationToken cancellationToken)
        {
            var userFile =
                await _context.UserGarages
                    .FirstOrDefaultAsync(
                    x => x.UserId.Equals(request.UserId) && x.Id.Equals(request.Id), cancellationToken);
            if (userFile == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }
            var entity = await CreateMessage(request, cancellationToken);
                Attachment attachment = new Attachment { Name = userFile.Path, UserGarageId = userFile.Id};
                attachment.MessageAttachments.Add(new MessageAttachment
                {
                    Message = entity,
                    Attachment = attachment
                });
                await _context.Attachments.AddAsync(attachment, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            return await _handler.Handle(new GetMessageByIdQuery { Id = entity.Id }, cancellationToken);
        }


        private async Task<Message> CreateMessage(FileForwardToMessageCommand request, CancellationToken cancellationToken)
        {
            Message message = new Message();
            message.RecipientGroupId = request.GroupId;
            message.SenderId = request.UserId;
            await _context.Messages.AddAsync(message, cancellationToken);
            var members = await _context.GroupMembers
                .Where(member => !member.MemberId.Equals(request.UserId) && member.GroupId.Equals(request.GroupId))
                .ToListAsync(cancellationToken);
            foreach (var member in members)
            {
                var entity = new UnreadMessage()
                {
                    Message = message,
                    SendUserId = request.UserId,
                    ReadUserId = member.MemberId,
                    GroupId = request.GroupId,
                };
                await _context.UnreadMessages.AddAsync(entity, cancellationToken);
            }

            return message;
        }
    }
}
