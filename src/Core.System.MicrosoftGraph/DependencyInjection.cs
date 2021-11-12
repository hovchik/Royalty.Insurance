using Core.System.MicrosoftGraph.MicrosoftOffice;
using Microsoft.Extensions.DependencyInjection;

namespace Core.System.MicrosoftGraph
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inject of delta
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMicrosoftGraph(this IServiceCollection services)
        {
            services.AddTransient<IGetUser, GetUser>();
            services.AddTransient<IGetMailFolder, GetMailFolder>();            
            services.AddTransient<IGetFolderContent, GetFolderContent>();
            services.AddTransient<IGetConversation, GetConversation>();
            services.AddTransient<ISendEmail, SendEmail>();
            services.AddTransient<IGetDefaultPrivateGroupId, GetDefaultPrivateGroupId>();
            services.AddTransient<IGetDocuments, GetDocuments>();
            services.AddTransient<IUploadDocument, UploadDocument>();
            services.AddTransient<IDownloadDocument, DownloadDocument>();

            return services;
        }
    }
}
