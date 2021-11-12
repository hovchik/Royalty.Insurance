using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Domain;

namespace Royalty.Insurance.BusinessLayer.Insureds
{
    public class UpdateInsuredCommandHandler : IRequestHandler<UpdateInsuredCommand, InsuredResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IInsuredMapperService _mapper;
        private readonly ICurrentUserService _currentUser;

        public UpdateInsuredCommandHandler(ICurrentUserService currentUser, IInsuredMapperService mapper, IApplicationDbContext context)
        {
            _currentUser = currentUser;
            _mapper = mapper;
            _context = context;
        }

        public async Task<InsuredResponse> Handle(UpdateInsuredCommand request, CancellationToken cancellationToken)
        {
            User user = await _context.Users
                .Where(item => item.Id.Equals(_currentUser.UserId))
                .FirstOrDefaultAsync(cancellationToken);
            if (user == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.EmailNotFound);
            }

            Insured entity = await _context.Insureds.Where(item => item.Id.Equals(request.Id)).FirstOrDefaultAsync(cancellationToken);
            entity.UpdatedBy = user.Id;
            _mapper.UpdateEntity(entity, request.Request);
            _context.Insureds.Update(entity);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _mapper.MapResponse.Invoke(entity);
        }
    }
}
