using MediatR;

namespace Royalty.Insurance.BusinessLayer.Cities.Queries
{
    public class GetCitiesQuery : IRequest<CityListViewModel>
    {
    }
}
