using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public class UserPhoneMapperService : IUserPhoneMapperService
    {
        public void UpdateEntity(UserPhone entity, UserPhoneRequest request)
        {
            entity.PhoneNumber = request.PhoneNumber;
            entity.IpAddress = request.IpAddress;
            entity.PhoneOwnerId = request.UserOwnerId;
            entity.Extension = request.Extension;
        }

        public Expression<Func<UserPhone, UserPhoneResponse>> MapResponse
        {
            get
            {
                return entity => new UserPhoneResponse
                {
                    Id=entity.Id,
                    IpAddress = entity.IpAddress,
                    Extension = entity.Extension,
                    UserOwnerId =  entity.PhoneOwnerId,
                    PhoneNumber = entity.PhoneNumber
                };
            }
        }
    }
}