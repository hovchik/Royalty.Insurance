using MediatR;

namespace Royalty.Insurance.BusinessLayer.Cargoes.Queries
{
    public class GetCargoesByIdQuery : IRequest<CargoListViewModel>
    {
        public int CargoId { get; set; }
    }
}