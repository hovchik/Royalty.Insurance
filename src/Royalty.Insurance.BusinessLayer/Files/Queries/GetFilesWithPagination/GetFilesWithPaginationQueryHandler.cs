using System.Common.Authentication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.Security.Cryptography;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.Extensions;
using Application.Interfaces;
using Royalty.Insurance.Proxy.Response;

namespace Royalty.Insurance.BusinessLayer.Files.Queries
{
    public class GetFilesWithPaginationQueryHandler : IRequestHandler<GetFilesWithPaginationQuery, PaginationResponse<UserFileResponse>>
    {
        //TODO make interface
        private readonly IApplicationDbContext _context;
        private readonly IExpiryQueryParameterCreator _expiryQueryParameterCreator;
        private readonly AppSetting _appSetting;
        private readonly IUserGarageMapperService _mapper;
        private readonly ICurrentUserService _currentUserService;

        public GetFilesWithPaginationQueryHandler(IApplicationDbContext context, 
            IUserGarageMapperService mapper, 
            ICurrentUserService currentUserService, 
            IExpiryQueryParameterCreator expiryQueryParameterCreator,
            IOptions<AppSetting> options)
        {
            _context = context;
            _mapper = mapper;
            _currentUserService = currentUserService;
            _expiryQueryParameterCreator = expiryQueryParameterCreator;
            _appSetting = options.Value;
        }

        public async Task<PaginationResponse<UserFileResponse>> Handle(GetFilesWithPaginationQuery request, CancellationToken cancellationToken)
        {
            request.EndDate = request.EndDate?.AddDays(1);// to include data for hat dat
            var entities = await _context.UserGarages
                .Include(item => item.AssignedInsured)
                .Where(item => item.UserId.Equals(_currentUserService.UserId)
                               && (string.IsNullOrEmpty(request.FileName) || item.Path.Contains(request.FileName))
                               && (!request.StartDate.HasValue || item.CreateDatetimeUtc > request.StartDate.Value)
                               && (!request.EndDate.HasValue || item.CreateDatetimeUtc < request.EndDate.Value)
                               && (request.FormatIds == null || request.FormatIds.Contains(item.FileFormatId))
                               && (!request.AssignedTo.HasValue || item.AssignedInsuredId == request.AssignedTo)

                )
                .OrderByDescending(item => item.CreateDatetimeUtc)
                .ToPaginationAsync(request.PageIndex, request.PageSize);
            var data = entities.Response
                .Select(item => _mapper.MapResponse.Invoke(item, _expiryQueryParameterCreator, _appSetting))
                .ToList();
            var response = new PaginationResponse<UserFileResponse>()
            {
                CurrentPage = entities.CurrentPage,
                PageCount = entities.PageCount,
                PageSize = entities.PageSize,
                Response = data,
                RowCount = entities.RowCount
            };

            return response;
        }
    }
}
