using System;
using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Core.System.MicrosoftGraph.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.Graph;
using Royalty.Insurance.Settings.Constants;

namespace Core.System.MicrosoftGraph.MicrosoftOffice
{
    public class GetConversation : IGetConversation
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        public GetConversation(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<IEnumerable<MicrosoftOfficeMessageResponse>> Handle(GetConversationRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);

            var response = await graphClient.Users[request.Email]
                .Messages
                .Request()
                .Filter(string.Format(MicrosoftOfficeConstants.ConversationIdFilter, request.ConversationId))
                .Header(MicrosoftOfficeConstants.GraphApiOfficeHeaderKey, MicrosoftOfficeConstants.GraphApiHeaderValue)
                .Select(MicrosoftOfficeConstants.GraphApiEmailSelector)
                .Skip(0)
                .Top(100)
                .GetAsync(cancellationToken);

            return response.Select(item => new MicrosoftOfficeMessageResponse
            {
                Body = item.UniqueBody.Content,
                CcRecipients = item.CcRecipients.Select(cc => cc.EmailAddress.Address),
                FromEmailAddress = item.From.EmailAddress.Address,
                ConversationId = item.ConversationId,
                HasAttachments = item.HasAttachments ?? false,
                SentDateTime = item.SentDateTime?.UtcDateTime ?? DateTime.UtcNow,
                Subject = item.Subject
            });
        }
    }
}
