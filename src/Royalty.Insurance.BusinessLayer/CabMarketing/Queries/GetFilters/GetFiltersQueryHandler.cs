using MediatR;
using Royalty.Insurance.Proxy.APIModels.Marketing;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetFiltersQueryHandler : IRequestHandler<GetFiltersQuery, List<string>>
    {
        public async Task<List<string>> Handle(GetFiltersQuery request, CancellationToken cancellationToken)
        {
            return typeof(CabExcelModel)
                .GetProperties()
                .Select(prop => prop.Name)
                .ToList();
        }
    }
}
