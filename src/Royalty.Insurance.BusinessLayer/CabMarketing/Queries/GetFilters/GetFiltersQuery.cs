using MediatR;
using System.Collections.Generic;

namespace Royalty.Insurance.BusinessLayer.CabMarketing
{
    public class GetFiltersQuery : IRequest<List<string>>
    {
    }
}
