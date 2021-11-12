using Application.Interfaces;
using Core.System.Security.Cryptography;
using Domain;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class UpdateUserByAdminCommandHandler : IRequestHandler<UpdateUserByAdminCommand, UserResponse>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAdminUserMapperService _adminMapper;
        private readonly IBaseUserProfileMapperService _baseUserMapper;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly AppSetting _appSetting;

        public UpdateUserByAdminCommandHandler(IAdminUserMapperService adminMapper, IApplicationDbContext context, IBaseUserProfileMapperService baseUserMapper, IOptions<AppSetting> appSetting, IExpiryQueryParameterCreator expiryQueryParameterCreator)
        {
            _adminMapper = adminMapper;
            _context = context;
            _baseUserMapper = baseUserMapper;
            _appSetting = appSetting.Value;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
        }

        public async Task<UserResponse> Handle(UpdateUserByAdminCommand request, CancellationToken cancellationToken)
        {
            User entity = await _context.Users.Where(item => item.Id.Equals(request.UserId)).FirstOrDefaultAsync();
            if (entity == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }
            _adminMapper.UpdateEntity(entity, request);
            _context.Users.Update(entity);
            if (await _context.SaveChangesAsync(new CancellationToken()) != 1)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, ResourceCommonMessage.SaveFailed);
            }

            return _baseUserMapper.MapResponse.Invoke(entity, _expiryQueryParameterCreator, _appSetting);
        }
    }
}
