using Royalty.Insurance.Proxy.Request;
using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;
using Domain;

namespace Royalty.Insurance.BusinessLayer.VehicleInfos
{
    public class VehicleInfoMapperService : IVehicleInfoMapperService
    {
        public void UpdateEntity(VehicleInfo entity, VehicleInfoRequest request)
        {
            entity.Type = request.Type;
            entity.ActualValue = request.ActualValue;
            entity.Comments = request.Comments;
            entity.Gvw = request.GVW;
            entity.Make = request.Make;
            entity.Radius = request.Radius;
            entity.Vin = request.VIN;
            entity.Year = request.Year;
            entity.IsTruck = request.IsTruck;
        }

        public Expression<Func<VehicleInfo, VehicleInfoResponse>> MapResponse
        {
            get
            {
                return entity => new VehicleInfoResponse
                {
                    Make = entity.Make,
                    Type = entity.Type,
                    GVW = entity.Gvw,
                    ActualValue = entity.ActualValue,
                    Radius = entity.Radius,
                    VIN = entity.Vin,
                    Comments = entity.Comments,
                    Year = entity.Year,
                    IsTruck = entity.IsTruck,
                    Id = entity.Id
                };
            }
        }
    }
}