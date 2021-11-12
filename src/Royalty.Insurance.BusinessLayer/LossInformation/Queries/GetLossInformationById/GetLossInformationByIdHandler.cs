using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class GetLossInformationByIdHandler : IRequestHandler<GetLossInformationById, LossInfoResponse>
    {
        private readonly ILossInfoMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetLossInformationByIdHandler(ILossInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LossInfoResponse> Handle(GetLossInformationById request, CancellationToken cancellationToken)
        {
            var entity = await _context.LossInformations
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