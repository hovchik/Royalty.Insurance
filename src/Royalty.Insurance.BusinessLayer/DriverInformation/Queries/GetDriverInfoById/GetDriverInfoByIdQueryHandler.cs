using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class GetDriverInfoByIdQueryHandler : IRequestHandler<GetDriverInfoByIdQuery, DriverInfoResponse>
    {
        private readonly IDriverInfoMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetDriverInfoByIdQueryHandler(IDriverInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<DriverInfoResponse> Handle(GetDriverInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await _context.DriverInformations
                .Where(item => item.Id.Equals(request.Id))
                .Select(_mapper.MapResponse)
                .FirstOrDefaultAsync(cancellationToken);

            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entity;
        }
    }
}