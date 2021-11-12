using System;
using System.Common.Authentication.Models;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Account
{
    public interface IAccountMapperService
    {
        void UpdateEntity(User entity, UserRequest request);
        Expression<Func<User, IExpiryQueryParameterCreator, AppSetting, LoginResponse>> MapResponse { get; }
    }
}
