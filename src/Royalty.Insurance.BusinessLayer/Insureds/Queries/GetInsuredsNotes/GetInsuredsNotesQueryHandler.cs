using Application.Interfaces;
using Domain;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Notes;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class GetInsuredsNotesQueryHandler : IRequestHandler<GetInsuredsNotesQuery, PaginationResponse<InsuredsNotesResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly INoteMapperService _mapper;

        public GetInsuredsNotesQueryHandler(IApplicationDbContext context, INoteMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<PaginationResponse<InsuredsNotesResponse>> Handle(GetInsuredsNotesQuery request, CancellationToken cancellationToken)
        {


            List<Note> result = await _context.Notes.Where(x => x.InsuredId != null).OrderBy(x => x.InsuredId).ToListAsync();
            var results = (from note in result
                           group note by note?.InsuredId into groupedNotes
                           join insured in _context.Insureds on groupedNotes.Key equals insured.Id
                           select new InsuredsNotesResponse
                           {
                               InsuredId = insured.Id,
                               InsuredName = insured.MailingName ?? insured.GaragingName,
                               AllNotes = groupedNotes.AsQueryable().Select(_mapper.MapResponse).OrderByDescending(x => x.CreatedDateTime).ToList()
                           }).ToList();
            var response = new PaginationResponse<InsuredsNotesResponse>
            {
                CurrentPage = request.PageIndex,
                PageSize = request.PageSize,
                RowCount = results.Count
            };

            var pageCount = (double)response.RowCount / request.PageSize;
            response.PageCount = (int)Math.Ceiling(pageCount);
            var skip = (request.PageIndex - 1) * request.PageSize;

            response.Response = results.Skip(skip) // default
                .Take(request.PageSize)
                .ToList();

            return response;
        }
    }
}
