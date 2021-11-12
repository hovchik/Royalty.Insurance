using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.MapperService;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class GetDriverInfoQueryHandler : IRequestHandler<GetDriverInfoQuery, DriverInfoListViewModel>
    {
        private readonly IDriverInfoMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetDriverInfoQueryHandler(IDriverInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DriverInfoListViewModel> Handle(GetDriverInfoQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.DriverInformations
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return new DriverInfoListViewModel
            {
                DriverInfo = entities
            };
        }
    }
}