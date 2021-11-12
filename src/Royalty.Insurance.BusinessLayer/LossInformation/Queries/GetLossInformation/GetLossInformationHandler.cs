using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class GetLossInformationHandler : IRequestHandler<GetLossInformation, LossInformationListViewModel>
    {
        private readonly ILossInfoMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public GetLossInformationHandler(ILossInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LossInformationListViewModel> Handle(GetLossInformation request, CancellationToken cancellationToken)
        {
            var entities = await _context.LossInformations
                .Select(_mapper.MapResponse)
                .ToListAsync(cancellationToken: cancellationToken);
            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return new LossInformationListViewModel
            {
                LossInformation = entities
            };
        }
    }
}