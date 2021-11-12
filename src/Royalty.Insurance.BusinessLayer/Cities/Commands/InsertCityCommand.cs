using MediatR;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Cities
{
    public class InsertCityCommand : IRequest<CityResponse>
    {
        public string Name { get; set; }
        public string State { get; set; }
    }
}
