using System;
using System.Linq.Expressions;
using Domain;
using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public interface IVehicleInfoMapperService
    {
        void UpdateEntity(VehicleInfo entity, VehicleInfoRequest request);
        Expression<Func<VehicleInfo, VehicleInfoResponse>> MapResponse { get; }
    }
}