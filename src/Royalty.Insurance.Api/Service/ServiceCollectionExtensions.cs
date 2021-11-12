using Core.System.Security.Cryptography;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Azure;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Royalty.Insurance.Common;
using Royalty.Insurance.Common.EventBrokers;
using Royalty.Insurance.MapperService;
using System;
using System.Common.Authentication;
using System.Common.Authentication.Models;
using System.Common.Authentication.Services;
using System.Common.Network;
using System.Common.Storage;
using System.Text;
using System.Threading.Tasks;

namespace Royalty.Insurance.Api.Service
{
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Inject Repository
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMapperService(this IServiceCollection services)
        {
            services.AddSingleton(typeof(IEventBroker<>), typeof(EventBroker<>));
            services.AddScoped<IZipMapperService, ZipMapperService>();

            return services;
        }

        public static IServiceCollection AddIntegrationServices(this IServiceCollection services, AppSetting appSetting)
        {
            services.AddTransient<IStorageManager, StorageManager>();

            services.AddAzureClients(builder =>
            {
                builder.AddBlobServiceClient(appSetting.BlobStorageConnectionString);
            });
            services.AddSingleton<IHttpHelper, HttpHelper>();
            services.AddTransient<IExpiryQueryParameterCreator>(item => new ExpiryQueryParameterCreator(appSetting.QueryParamSecret));

            return services;
        }

        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, JwtTokenConfig jwtTokenConfig)
        {

            //string schemeName = nameof(SessionHashAuthenticationSchemeOptions);
            services.AddSingleton(jwtTokenConfig);
            services.AddAuthentication(options =>
            {
                //options.DefaultScheme = schemeName;
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(option =>
            {

                option.RequireHttpsMetadata = true;
                option.SaveToken = true;
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = jwtTokenConfig.Issuer,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtTokenConfig.Secret)),
                    ValidAudience = jwtTokenConfig.Audience,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.FromMinutes(2)
                };
                option.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        // If the request is for our hub...
                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken) &&
                            (path.StartsWithSegments("/messagehub")))
                        {
                            // Read the token out of the query string
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            //.AddScheme<SessionHashAuthenticationSchemeOptions, SessionAuthenticationHandler>(schemeName, op => { })

            services.AddSingleton<IJwtAuthManager, JwtAuthManager>();
            services.AddAuthorization();
            //services.AddAuthorization(options =>
            //{
            //    var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(schemeName);
            //    defaultAuthorizationPolicyBuilder =
            //        defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
            //    options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            //});

            return services;
        }
    }
}
