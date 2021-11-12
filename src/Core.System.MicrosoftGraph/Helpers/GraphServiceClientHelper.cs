using System.Collections.Generic;
using System.Common.Authentication.Models;
using System.Common.Extensions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
using Microsoft.Graph.Auth;
using Microsoft.Identity.Client;

namespace Core.System.MicrosoftGraph.Helpers
{
    public static class GraphServiceClientHelper
    {

        public static GraphServiceClient GetGraphServiceClient(MicrosoftOfficeSetting microsoftOfficeSetting)
        {
            IConfidentialClientApplication confidentialClientApplication = ConfidentialClientApplicationBuilder
                .Create(microsoftOfficeSetting.ClientId)
                .WithTenantId(microsoftOfficeSetting.TenantId)
                .WithClientSecret(microsoftOfficeSetting.ClientSecret)
                .Build();

            ClientCredentialProvider authProvider = new ClientCredentialProvider(confidentialClientApplication);

            return new GraphServiceClient(authProvider);
        }

        public static async Task<MessageAttachmentsCollectionPage> GetAttachments(List<IFormFile> attachments)
        {
            var messageAttachments = new MessageAttachmentsCollectionPage();
            foreach (var attachment in attachments)
            {
                messageAttachments.Add(new FileAttachment
                {
                    Name = attachment.FileName,
                    ContentType = attachment.ContentType,
                    ContentBytes = await attachment.GetBytes()
                });
            }

            return messageAttachments;
        }

        public static IDriveItemChildrenCollectionRequest SkipToken(this IDriveItemChildrenCollectionRequest request, string skipToken)
        {
            if (!string.IsNullOrEmpty(skipToken))
                request.QueryOptions.Add(new QueryOption("$skiptoken", skipToken));

            return request;
        }

        public static IDriveItemChildrenCollectionRequest FilterStartWith(
            this IDriveItemChildrenCollectionRequest request, string key, string value)
        {
            if (!string.IsNullOrEmpty(value) && !string.IsNullOrEmpty(key))
            {
                request.Filter($"startswith({key},'{value}')");
            }

            return request;
        }
    }
}
