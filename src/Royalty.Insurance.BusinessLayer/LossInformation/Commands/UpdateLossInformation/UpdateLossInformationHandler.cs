using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public class UpdateLossInformationHandler : IRequestHandler<UpdateLossInformationCommand, LossInfoResponse>
    {
        private readonly ILossInfoMapperService _mapper;
        private readonly IApplicationDbContext _context;

        public UpdateLossInformationHandler(ILossInfoMapperService mapper, IApplicationDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<LossInfoResponse> Handle(UpdateLossInformationCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.LossInformations.Where(item => item.Id.Equals(request.Id)).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            _mapper.UpdateEntity(entity, request);
            _context.LossInformations.Update(entity);

            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}