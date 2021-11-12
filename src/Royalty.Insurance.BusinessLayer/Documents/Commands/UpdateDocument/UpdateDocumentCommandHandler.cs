using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, DocumentResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;

        public UpdateDocumentCommandHandler(IApplicationDbContext context, IDocumentMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DocumentResponse> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var document =
                await _context.Documents.FirstOrDefaultAsync(item => item.Id.Equals(request.Id), cancellationToken);
            if (document == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            if (document.InsuredId.HasValue)
            {
                throw new RestApiResponseException(ResourceCommonMessage.DocumentAlreadyHasInsured);
            }

            document.InsuredId = request.InsuredId;
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw  new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(document);
        }
    }
}
