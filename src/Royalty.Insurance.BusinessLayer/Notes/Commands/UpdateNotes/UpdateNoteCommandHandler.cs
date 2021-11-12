using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Notes
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, NoteResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly INoteMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateNoteCommandHandler(ICurrentUserService currentUser, IApplicationDbContext context, INoteMapperService mapper)
        {
            _currentUser = currentUser;
            _context = context;
            _mapper = mapper;
        }

        public async Task<NoteResponse> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Notes.FirstOrDefaultAsync(note => note.Id == request.Id && note.UserId == _currentUser.UserId, cancellationToken);
            if(entity==null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound,
                   ResourceCommonMessage.EntityNotFound);
            }

            _mapper.UpdateEntity(entity, request.Request);
            _context.Notes.Update(entity);
            await _context.SaveChangesAsync(cancellationToken);

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
