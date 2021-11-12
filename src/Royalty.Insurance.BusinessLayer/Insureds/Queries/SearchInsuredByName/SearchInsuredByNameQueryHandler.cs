using MediatR;
using Microsoft.EntityFrameworkCore;
using Application.Interfaces;
using Royalty.Insurance.Settings.Enums;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Insureds.Queries
{
    public class SearchInsuredByNameQueryHandler : IRequestHandler<SearchInsuredByNameQuery, InsuredListViewModel>
    {
        private readonly IApplicationDbContext _context;

        public SearchInsuredByNameQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<InsuredListViewModel> Handle(SearchInsuredByNameQuery request, CancellationToken cancellationToken)
        {
            var insureds = await _context.Insureds.Where(item =>
                    item.GaragingName.Contains(request.SearchTerm) || item.MailingName.Contains(request.SearchTerm))
                .Select(item => new BaseInsuredResponse
                {
                    Id = item.Id,
                    Name = item.LegalStatusId == (int)LegalStatusType.Individual ? item.MailingName : item.GaragingName
                })
                .ToListAsync(cancellationToken);

            return new InsuredListViewModel
            {
                Insureds = insureds
            };
        }
    }
}
