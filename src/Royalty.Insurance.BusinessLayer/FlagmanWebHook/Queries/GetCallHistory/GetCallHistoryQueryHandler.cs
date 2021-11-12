using MediatR;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using Royalty.Insurance.Settings.Enums;
using System;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook.Queries.GetCallHistory
{
    public class GetCallHistoryQueryHandler : IRequestHandler<GetCallHistoryQuery, PaginationResponse<UserPhoneCallHistoryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneCallHistoryMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetCallHistoryQueryHandler(IUserPhoneCallHistoryMapperService mapper, IApplicationDbContext context, ICurrentUserService currentUserService)
        {
            _mapper = mapper;
            _context = context;
            _currentUserService = currentUserService;
        }

        public async Task<PaginationResponse<UserPhoneCallHistoryResponse>> Handle(GetCallHistoryQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.UserPhoneCallHistories.Where(item => item.UserPhoneId.Equals(_currentUserService.UserId)
                                     && item.InitialCallTypeId != (int)CallTypeCode.Established
                                     && DateTime.UtcNow.Day - item.CreateDatetimeUtc.Day < 10)
                                     .OrderByDescending(item => item.CreateDatetimeUtc)
                                     .ToPaginationAsync(_mapper.MapResponse, request.PageIndex, request.PageSize);

            if(entities.RowCount==0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return entities;
        }
    }
}