using Microsoft.Extensions.DependencyInjection;

namespace Core.System.Delta
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inject of delta
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddDelta(this IServiceCollection services)
        {
            services.AddSingleton<IBaseDeltaRequestHandler, BaseDeltaRequestHandler>();
            services.AddTransient<INewBillingAccount, NewBillingAccount>();
            services.AddTransient<IAdditionalPremiumEndorsement, AdditionalPremiumEndorsement>();
            services.AddTransient<IReturnPremiumEndorsement, ReturnPremiumEndorsement>();
            services.AddTransient<IPolicyCancellation, PolicyCancellation>();
            services.AddTransient<IPolicyReinstatement, PolicyReinstatement>();
            services.AddTransient<IBillingAccountInformation, BillingAccountInformation>();
            services.AddTransient<IInsuredInformationChange, InsuredInformationChange>();
            services.AddTransient<IAgentInformationChange, AgentInformationChange>();

            return services;
        }
    }
}
