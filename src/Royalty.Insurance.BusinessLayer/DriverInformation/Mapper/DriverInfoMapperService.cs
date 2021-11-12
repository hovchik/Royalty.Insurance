using Royalty.Insurance.Proxy.Response;
using System;
using System.Linq.Expressions;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class DriverInfoMapperService : IDriverInfoMapperService
    {
        public Expression<Func<Domain.DriverInformation, DriverInfoResponse>> MapResponse
        {
            get
            {
                return entity => new DriverInfoResponse
                {
                    Accidents = entity.Accidents,
                    YearOfExperiance = entity.YearOfExperiance,
                    LicenseNumber = entity.LicenseNumber,
                    DateHired = entity.DateHired,
                    DateOfBirth = entity.DateOfBirth,
                    DriverName = entity.DriverName
                };
            }
        }

        public void UpdateEntity(Domain.DriverInformation entity, UpdateDriverInfoCommand request)
        {
            entity.Accidents = request.Accidents;
            entity.DateHired = request.DateHired;
            entity.DateOfBirth = request.DateOfBirth;
            entity.DriverName = request.DriverName;
            entity.LicenseNumber = request.LicenseNumber;
            entity.YearOfExperiance = request.YearOfExperiance;
        }
    }
}
