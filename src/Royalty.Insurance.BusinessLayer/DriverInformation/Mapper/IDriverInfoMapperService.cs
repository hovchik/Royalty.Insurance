using System;
using System.Linq.Expressions;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public interface IDriverInfoMapperService
    {
        void UpdateEntity(Domain.DriverInformation entity, UpdateDriverInfoCommand request);
        Expression<Func<Domain.DriverInformation, DriverInfoResponse>> MapResponse { get; }
    }
}
