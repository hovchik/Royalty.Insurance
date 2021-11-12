using System;
using System.Collections.Generic;
using System.Text;

namespace Royalty.Insurance.Proxy.Response
{
    public class DriverInfoResponse
    {
        public string DriverName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string LicenseNumber { get; set; }
        public DateTime DateHired { get; set; }
        public int YearOfExperiance { get; set; }
        public string Accidents { get; set; }
    }
}
