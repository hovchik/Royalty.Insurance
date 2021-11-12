using MediatR;

namespace Royalty.Insurance.BusinessLayer.Insureds.Queries
{
    public class SearchInsuredByNameQuery : IRequest<InsuredListViewModel>
    {
        public string SearchTerm { get; set; }
    }
}
