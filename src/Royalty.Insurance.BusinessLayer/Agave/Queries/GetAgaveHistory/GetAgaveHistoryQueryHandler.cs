using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class GetAgaveHistoryQueryHandler : IRequestHandler<GetAgaveHistoryQuery, PaginationResponse<AgaveRoyaltyResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAgaveSaleMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetAgaveHistoryQueryHandler(IApplicationDbContext context, IAgaveSaleMapperService mapper, ICurrentUserService currentUserService)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<PaginationResponse<AgaveRoyaltyResponse>> Handle(GetAgaveHistoryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.AgaveSalesHistories.Where(item => item.UserId.Equals(_currentUserService.UserId)
                                                                            && (!request.TransactionTypes.HasValue || (int)request.TransactionTypes == item.TransactionTypeId)
                                                                            && (!request.From.HasValue || request.From.Value <= item.CreateDateTimeUtc)
                                                                            && (!request.To.HasValue || request.To.Value >= item.CreateDateTimeUtc)
                                                                            && (string.IsNullOrEmpty(request.CardHolderName) || item.CardHolderName.ToUpper().Contains(request.CardHolderName.ToUpper())))
                .OrderByDescending(item => item.CreateDateTimeUtc)
                .ToPaginationAsync(_mapper.MapSalesResponse, request.PageIndex, request.PageSize);

            return entities;
        }
    }
}