using System;
using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.DriverInfo
{
    public class UpdateDriverInfoCommand : IRequest<DriverInfoResponse>
    {
        public int Id { get; set; }
        public string DriverName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime DateHired { get; set; }
        public int YearOfExperiance { get; set; }
        public string Accidents { get; set; }
    }
}