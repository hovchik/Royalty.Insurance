using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.Proxy.Response;
using Royalty.Insurance.Settings;
using System.Collections.Generic;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.FlagmanWebHook
{
    public class GetFilteredCallLogsQueryHandler : IRequestHandler<GetFilteredCallLogsQuery, List<UserPhoneCallHistoryResponse>>
    {
        private readonly IApplicationDbContext _context;
        private readonly IUserPhoneCallHistoryMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetFilteredCallLogsQueryHandler(ICurrentUserService currentUserService, IUserPhoneCallHistoryMapperService mapper, IApplicationDbContext context)
        {
            _currentUserService = currentUserService;
            _mapper = mapper;
            _context = context;
        }

        public async Task<List<UserPhoneCallHistoryResponse>> Handle(GetFilteredCallLogsQuery request, CancellationToken cancellationToken)
        {
            var filteredLogs = await _context
               .UserPhoneCallHistories
               .Where(log => log.UserPhoneId == _currentUserService.UserId &&
                   (!request.To.HasValue || log.CreateDatetimeUtc < request.To.Value) &&
                   (!request.From.HasValue || log.CreateDatetimeUtc > request.From.Value) &&
                   (!request.Extension.HasValue || log.Extension == request.Extension.Value) &&
                   (!request.CallType.HasValue || log.CurrentCallTypeId == (int)request.CallType.Value) &&
                   (string.IsNullOrEmpty(request.CallNumber) || log.CallerNumber.Contains(request.CallNumber)) &&
                   (string.IsNullOrEmpty(request.CallerName) || log.CallerName.Contains(request.CallerName))).Select(_mapper.MapResponse).ToListAsync();

            if (filteredLogs.Count == 0)
            {
                throw new RestApiResponseException((int)HttpStatusCode.NotFound, ResourceCommonMessage.EntityNotFound);
            }

            return filteredLogs;
        }
    }
}