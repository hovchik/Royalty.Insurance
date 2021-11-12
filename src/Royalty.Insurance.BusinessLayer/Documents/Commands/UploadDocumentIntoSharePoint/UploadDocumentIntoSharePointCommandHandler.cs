using System.Common.Exceptions;
using System.Net;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph;
using LinqKit;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class UploadDocumentIntoSharePointCommandHandler : IRequestHandler<UploadDocumentIntoSharePointCommand, DocumentResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;
        private readonly IGetDefaultPrivateGroupId _defaultPrivateGroup;
        private readonly IUploadDocument _uploadDocument;

        public UploadDocumentIntoSharePointCommandHandler(IApplicationDbContext context, IUploadDocument uploadDocument, IGetDefaultPrivateGroupId defaultPrivateGroup, IDocumentMapperService mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _uploadDocument = uploadDocument;
            _defaultPrivateGroup = defaultPrivateGroup;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<DocumentResponse> Handle(UploadDocumentIntoSharePointCommand request, CancellationToken cancellationToken)
        {
            var uploadRequest = new UploadDocumentRequest
            {
                FileName = request.DocumentFile.FileName,
                GroupId = await _defaultPrivateGroup.Handle(cancellationToken),
                DocumentStream = request.DocumentFile.OpenReadStream()
            };

            var response = await _uploadDocument.Handle(uploadRequest, cancellationToken);
            var entity = new Document { CreatedBy = _currentUserService.UserId };
            _mapper.UpdateEntity(entity, request.InsuredId, request.DocumentFile.FileName, response, _currentUserService.UserId);
            await _context.Documents.AddAsync(entity, cancellationToken);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
