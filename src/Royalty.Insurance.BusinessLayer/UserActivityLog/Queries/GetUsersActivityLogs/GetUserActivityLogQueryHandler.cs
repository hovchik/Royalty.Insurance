using Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Royalty.Insurance.BusinessLayer.Extensions;
using Royalty.Insurance.Proxy.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Royalty.Insurance.BusinessLayer.UserActivityLogs
{
    public class GetUserActivityLogQueryHandler : IRequestHandler<GetUserActivityLogQuery, PaginationResponse<UserActivityLogResponse>>
    {
        private readonly IApplicationDbContext _context;

        public GetUserActivityLogQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<PaginationResponse<UserActivityLogResponse>> Handle(GetUserActivityLogQuery request, CancellationToken cancellationToken)
        {
            var entities = await _context.UserActivityLogs.Where(item => item.LogOutDatetimeUtc.HasValue)
                .Select(item => new
                {
                    item.UserId,
                    Duration = EF.Functions.DateDiffMinute(item.LogInDatetimeUtc, item.LogOutDatetimeUtc),
                    item.LogInDatetimeUtc.Date,

                }).
                GroupBy(item => new { item.UserId, item.Date })
                .Select(item => new
                {
                    item.Key.UserId,
                    Login = item.Min(l => l.Date),
                    Duration = item.Sum(s => s.Duration.Value)
                })
                .Join(_context.Users, grouping => grouping.UserId, user => user.Id,
                    (grouping, user) => new UserActivityLogResponse
                    {
                        UserId = grouping.UserId,
                        Login = grouping.Login,
                        Duration = grouping.Duration,
                        FullName = user.FirstName + " " + user.LastName,
                    })
                .OrderByDescending(item => item.FullName)
                .ToPaginationAsync(request.PageIndex, request.PageSize);

            return entities;
        }
    }
}
