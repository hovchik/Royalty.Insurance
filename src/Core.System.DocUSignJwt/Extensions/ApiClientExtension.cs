using Core.System.DocUSignJwt.Model;
using DocuSign.eSign.Client;
using Royalty.Insurance.Settings;

namespace Core.System.DocUSignJwt.Extensions
{
    public static class ApiClientExtension 
    {
        public static void SetAuthorization(this ApiClient appClient, User user)
        {
            appClient.Configuration.DefaultHeader.Clear();
            appClient.Configuration.DefaultHeader.Add("Authorization", $"{SystemConstants.AuthenticationType} {user.AccessToken}");
        }
    }
}
