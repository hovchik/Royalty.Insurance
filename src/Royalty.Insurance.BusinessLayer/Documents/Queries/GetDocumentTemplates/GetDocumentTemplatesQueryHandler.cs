using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Documents
{
    public class GetDocumentTemplatesQueryHandler : IRequestHandler<GetDocumentTemplatesQuery, DocumentListViewModel>
    {
        private readonly IApplicationDbContext _context;
        private readonly IDocumentMapperService _mapper;

        public GetDocumentTemplatesQueryHandler(IApplicationDbContext context, IDocumentMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<DocumentListViewModel> Handle(GetDocumentTemplatesQuery request, CancellationToken cancellationToken)
        {
            var templates = new List<byte>{ (byte)DocumentTypeCode.RoyaltyForms, (byte)DocumentTypeCode.Supplement, (byte)DocumentTypeCode.AccordForms} ;
            var entities = await _context.Documents
                .Where(item => !item.IsDeleted && templates.Contains(item.DocumentTypeId))
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken);


            return new DocumentListViewModel{Documents = entities,} ;
        }
    }
}
