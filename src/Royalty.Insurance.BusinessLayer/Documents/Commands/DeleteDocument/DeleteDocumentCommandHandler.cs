using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Settings;
using System.Common.Constants;
using System.Common.Exceptions;
using System.Common.Storage;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IStorageManager _storageManager;
        private readonly ICurrentUserService _currentUserService;
        private readonly IDocumentMapperService _mapper;

        public DeleteDocumentCommandHandler(IApplicationDbContext context, IStorageManager storageManager, ICurrentUserService currentUserService, IDocumentMapperService mapper)
        {
            _context = context;
            _storageManager = storageManager;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var document = await _context.Documents.Where(x => x.Path.Equals(request.FileName) && x.InsuredId == request.InsuredId && x.IsDeleted == false)
                .FirstOrDefaultAsync(cancellationToken);
            var response = await _storageManager.DeleteAsync(Constants.Documents, request.InsuredId.ToString(), request.FileName, _currentUserService.UserId);
            _mapper.UpdateEntity(document, response);
            _context.Documents.Update(document);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                    ResourceCommonMessage.DeleteFailed);
            }

            return Unit.Value;
        }
    }
}
