using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;
using Royalty.Insurance.Settings;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public class GetUser : IGetUser
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        public GetUser(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<MicrosoftOfficeUserResponse> Handle(GetUserRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);
            var response = await graphClient.Users
                .Request()
                .Filter($"mail eq '{request.Email}'")
                .GetAsync(cancellationToken);
            var user = response.FirstOrDefault();
            if (user == null)
            {
                throw new RestApiResponseException(ResourceCommonMessage.UserNotFound);
            }

            return new MicrosoftOfficeUserResponse
            {
                Email = user.Mail
            };
        }
    }
}
