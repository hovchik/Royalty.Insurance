using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.UserPhoneSettings
{
    public interface IUserPhoneMapperService
    {
        void UpdateEntity(UserPhone entity, UserPhoneRequest request);
        Expression<Func<UserPhone, UserPhoneResponse>> MapResponse { get; }
    }
}