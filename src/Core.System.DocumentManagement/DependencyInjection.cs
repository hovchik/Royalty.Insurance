using Core.System.DocumentManagement.Manager;
using Core.System.DocumentManagement.Mediator;
using Microsoft.Extensions.DependencyInjection;

namespace Core.System.DocumentManagement
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inject of delta
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDocumentManagement(this IServiceCollection services)
        {
            services.AddTransient<IDocumentManager, DocumentManager>();
            services.AddTransient<IGenerateDocumentMediator, GenerateDocumentMediator>();

            return services;
        }
    }
}
