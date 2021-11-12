using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, DocumentPaginationViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;

        public GetDocumentsQueryHandler(IGetDocuments getDocuments, IApplicationDbContext context, IDocumentMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DocumentPaginationViewModel> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var documents = await _context
                .Documents
                .Where(document => !document.IsDeleted &&
                    (!request.StartDate.HasValue || document.CreateDatetimeUtc < request.StartDate.Value) &&
                    (!request.EndDate.HasValue || document.CreateDatetimeUtc > request.EndDate.Value) &&
                    (string.IsNullOrEmpty(request.FileName) || document.DocumentName.Contains(request.FileName))
                )
                .OrderByDescending(document => document.CreateDatetimeUtc)
                .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);


            return new DocumentPaginationViewModel
            {
                Documents = documents,
            };
        }
    }
}
