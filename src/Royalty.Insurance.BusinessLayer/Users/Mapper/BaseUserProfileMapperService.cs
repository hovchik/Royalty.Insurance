using System;
using System.Common.Authentication.Models;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public class BaseUserProfileMapperService : IBaseUserProfileMapperService
    {
        public Expression<Func<User, IExpiryQueryParameterCreator, AppSetting, UserResponse>> MapResponse
        {
            get
            {
                return (entity, expiryQueryParameterCreator, appSetting) => new UserResponse(expiryQueryParameterCreator, appSetting)
                {
                    UserId = entity.Id,
                    Email = entity.Email,
                    IsActive = entity.IsActive,
                    CellPhone = entity.CellPhone,
                    FirstName = entity.FirstName,
                    HomePhone = entity.HomePhone,
                    LastName = entity.LastName,
                    WorkPhone = entity.WorkPhone,
                    PersonalAvatar = entity.PersonalAvatar,
                    Role = (UserRoleType)entity.UserRoleId,
                    Status = entity.UsersProfile == null ? (int)UserStatusCode.Offline : entity.UsersProfile.UserStatusId,
                    CustomStatus = entity.UsersProfile == null ?  null: entity.UsersProfile.Status
                };
            }
        }
    }
}
