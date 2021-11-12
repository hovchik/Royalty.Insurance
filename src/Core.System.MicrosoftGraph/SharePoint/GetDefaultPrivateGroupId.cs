using System;
using System.Common.Authentication.Models;
using System.Common.Exceptions;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;


namespace Core.System.MicrosoftGraph
{
    public class GetDefaultPrivateGroupId : IGetDefaultPrivateGroupId
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;

        public GetDefaultPrivateGroupId(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }
        public async Task<string> Handle(CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);
            var groups = await graphClient.Groups // mailbox groups
                .Request()
                .GetAsync(cancellationToken);

            var group = groups.FirstOrDefault(item => item.Visibility == "Private");// we will have only one group

            if (group == null)
            {
                throw new RestApiResponseException((int)HttpStatusCode.InternalServerError, "Sharepoint default private group need to be created.");
            }

            return group.Id;
        }
    }
}
