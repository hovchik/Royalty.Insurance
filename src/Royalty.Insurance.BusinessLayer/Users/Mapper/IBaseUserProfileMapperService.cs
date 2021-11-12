using System;
using System.Common.Authentication.Models;
using System.Linq.Expressions;
using Core.System.Security.Cryptography;
using Domain;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Users
{
    public interface IBaseUserProfileMapperService
    {
        Expression<Func<User, IExpiryQueryParameterCreator, AppSetting, UserResponse>> MapResponse { get; }
    }
}
