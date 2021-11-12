using System;
using System.Linq.Expressions;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.LossInfo
{
    public interface ILossInfoMapperService
    {
        void UpdateEntity(Domain.LossInformation entity, UpdateLossInformationCommand request);
        Expression<Func<Domain.LossInformation, LossInfoResponse>> MapResponse { get; }
    }
}