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
    public class GetFolderContent : IGetFolderContent
    {
        private readonly MicrosoftOfficeSetting _microsoftOfficeSetting;
        public GetFolderContent(IOptions<AppSetting> options)
        {
            _microsoftOfficeSetting = options.Value.MicrosoftOfficeSetting;
        }

        public async Task<IEnumerable<MicrosoftOfficeMessageResponse>> Handle(GetFolderContentRequest request, CancellationToken cancellationToken)
        {
            var graphClient = GraphServiceClientHelper.GetGraphServiceClient(_microsoftOfficeSetting);

            var response = await graphClient.Users[request.Email]
                .Messages
                .Request()
                .Filter(string.Format(MicrosoftOfficeConstants.ParentFolderFilter, request.ParentFolderId))
                .Header(MicrosoftOfficeConstants.GraphApiOfficeHeaderKey, MicrosoftOfficeConstants.GraphApiHeaderValue)
                .Select(MicrosoftOfficeConstants.GraphApiEmailSelector)
                .Skip(0)
                .Top(100)
                .GetAsync(cancellationToken);

            var result = response.GroupBy(item => item.ConversationId)
                .Select(item => new
                {
                    ConversationId = item.Key,
                    LastMessage = item.OrderByDescending(message => message.SentDateTime).First()
                })
                .Select(item => new MicrosoftOfficeMessageResponse
                {
                    Body = item.LastMessage.UniqueBody.Content,
                    SentDateTime = item.LastMessage.SentDateTime?.UtcDateTime,
                    ConversationId = item.ConversationId,
                    HasAttachments = item.LastMessage.HasAttachments,
                    CcRecipients = item.LastMessage.CcRecipients.Select(cc => cc.EmailAddress.Address),
                    FromEmailAddress = item.LastMessage.From.EmailAddress.Address,
                    Subject = item.LastMessage.Subject,
                    FolderId = item.LastMessage.ParentFolderId,
                    IsRead = item.LastMessage.IsRead
                })
                .ToList();


            return result;
        }
    }
}
