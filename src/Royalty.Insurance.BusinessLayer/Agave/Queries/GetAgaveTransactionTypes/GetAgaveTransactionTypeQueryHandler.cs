using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class GetAgaveTransactionTypeQueryHandler : IRequestHandler<GetAgaveTransactionTypeQuery, List<AgaveTransactionTypeResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgaveSaleMapperService _mapper;

        public GetAgaveTransactionTypeQueryHandler(IApplicationDbContext context, IAgaveSaleMapperService mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<AgaveTransactionTypeResponse>> Handle(GetAgaveTransactionTypeQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.AgaveTransactionTypes.Select(_mapper.MapTransactionTypes).ToListAsync(cancellationToken);

            return entities;
        }
    }
}