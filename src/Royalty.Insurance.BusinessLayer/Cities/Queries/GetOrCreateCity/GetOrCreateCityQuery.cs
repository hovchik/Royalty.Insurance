using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Cities.Queries
{
    public class GetOrCreateCityQuery : IRequest<CityResponse>
    {
        public string StateName { get; set; }

        public string Name { get; set; }
    }
}
