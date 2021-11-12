using LinqKit;
using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Roles;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, NoteResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly INoteMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateNoteCommandHandler(INoteMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUser)
        {
            _mapper = mapper;
            _context = context;
            _currentUser = currentUser;
        }

        public async Task<NoteResponse> Handle(CreateNoteCommand request, CancellationToken cancellationToken)
        {
            Note entity = new Note();
            _mapper.UpdateEntity(entity, request.Request);
            entity.UserId = _currentUser.UserId;

            await _context.Notes.AddAsync(entity, cancellationToken);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                    ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
