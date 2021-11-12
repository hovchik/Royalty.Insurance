using MediatR;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.VinCheck
{
    public class GetVinInfoQuery : IRequest<List<VinCheckResponse>>
    {
        public string VinNumber { get; set; }
    }
}