using System;
using MediatR;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings.Enums;

namespace Royalty.Insurance.BusinessLayer.Agave
{
    public class GetAgaveHistoryQuery : IRequest<PaginationResponse<AgaveRoyaltyResponse>>
    {
        public int? UserId { get; set; }
        public DateTime? From { get; set; }
        public DateTime? To { get; set; }
        public AgaveTransactionTypes? TransactionTypes { get; set; }
        public string CardHolderName { get; set; }
        public int PageIndex { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}