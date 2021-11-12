using System.Common.Authentication.Models;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.MicrosoftOffice;
using MediatR;
using Microsoft.Extensions.Options;
namespace Royalty.Insurance.BusinessLayer.MicrosoftOffice
{
    public class GetUserQueryHandler : IRequestHandler<GetUserQuery, MicrosoftOfficeUserResponse>
    {
        private readonly IGetUser _getUser;
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        public GetUserQueryHandler(IOptions<AppSetting> options, IGetUser getUser)
        {
            _getUser = getUser;
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }


        public async Task<MicrosoftOfficeUserResponse> Handle(GetUserQuery request, CancellationToken cancellationToken) =>
            await _getUser.Handle(request, cancellationToken);
    }
}
