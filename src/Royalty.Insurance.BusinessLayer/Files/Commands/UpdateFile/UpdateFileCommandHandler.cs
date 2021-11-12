using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.System.Security.Cryptography;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.BusinessLayer.Files
{
    public class UpdateFileCommandHandler : IRequestHandler<UpdateFileCommand, UserFileResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly ICurrentUserService _currentUserService;
        private readonly AppSetting _appSetting;
        private readonly IUserGarageMapperService _mapper;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;

        public UpdateFileCommandHandler(IApplicationDbContext context, ICurrentUserService currentUserService, 
            IUserGarageMapperService mapper, 
            IExpiryQueryParameterCreator expiryQueryParameterCreator,
            IOptions<AppSetting> appSetting)
        {
            _context = context;
            _currentUserService = currentUserService;
            _mapper = mapper;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _appSetting = appSetting.Value;
        }

        public async Task<UserFileResponse> Handle(UpdateFileCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.UserGarages
                .Include(item => item.AssignedInsured).FirstOrDefaultAsync(
                item => item.Id.Equals(request.Id) && item.UserId.Equals(_currentUserService.UserId),
                cancellationToken);
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            entity.AssignedInsuredId = request.AssignToId;
            _context.UserGarages.Update(entity);
            if (await _context.SaveChangesAsync(cancellationToken) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError,
                    ResourceCommonMessage.SaveFailed);
            }
            //TODo change this
            entity = await _context.UserGarages
                .Include(item => item.AssignedInsured).FirstOrDefaultAsync(
                    item => item.Id.Equals(request.Id) && item.UserId.Equals(_currentUserService.UserId),
                    cancellationToken);

            return _mapper.MapResponse.Invoke(entity, _expiryQueryParameterCreator, _appSetting);
        }
    }
}
