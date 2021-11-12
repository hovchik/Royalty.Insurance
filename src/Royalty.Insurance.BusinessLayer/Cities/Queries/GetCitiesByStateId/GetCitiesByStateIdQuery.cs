using MediatR;

namespace Royalty.Insurance.BusinessLayer.Cities.Queries
{
    public class GetCitiesByStateIdQuery : IRequest<CityListViewModel>
    {
        public int StateId { get; set; }
    }
}
