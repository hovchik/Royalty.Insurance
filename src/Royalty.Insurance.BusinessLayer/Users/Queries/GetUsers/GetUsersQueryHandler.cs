using Application.Interfaces;
using Core.System.Security.Cryptography;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class GetUsersQueryHandler : IRequestHandler<GetUsersQuery, List<UserResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly AppSetting _appSetting;

        public GetUsersQueryHandler(IExpiryQueryParameterCreator expiryQueryParameterCreator, IOptions<AppSetting> appSetting, IApplicationDbContext context)
        {
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _context = context;
            _appSetting = appSetting.Value;
        }

        public async Task<List<UserResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
        {
            var entities = await (from u in _context.Users
                                  join up in _context.UserPhones on u.Id equals up.PhoneOwnerId into joined
                                  from subUser in joined.DefaultIfEmpty()
                                  where u.IsActive
                                  select new UserResponse(_expiryQueryParameterCreator, _appSetting)
                                  {
                                      Extension = subUser.Extension,
                                      FirstName = u.FirstName,
                                      PersonalAvatar = u.PersonalAvatar,
                                      IsActive = u.IsActive,
                                      LastName = u.LastName,
                                      HomePhone = u.HomePhone,
                                      IpAddress = subUser.IpAddress,
                                      Email = u.Email,
                                      Role = (UserRoleType)u.UserRoleId,
                                      CellPhone = u.CellPhone,
                                      WorkPhone = u.WorkPhone,
                                      Status = u.UsersProfile == null ? (int)UserStatusCode.Offline : u.UsersProfile.UserStatusId,
                                      CustomStatus = u.UsersProfile == null ? null : u.UsersProfile.Status,
                                      UserId = u.Id
                                  }).ToListAsync(cancellationToken);

            if (entities.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}
