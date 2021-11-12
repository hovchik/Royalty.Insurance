using System;
using System.Common.Authentication.Models;
using System.Common.Authentication.TwoFactor;
using System.Common.Handler;
using System.Common.Middleware;
using System.Common.Services;
using System.IO;
using System.Reflection;
using System.Security.Claims;
using AspNetCore.Totp;
using AspNetCore.Totp.Interface;
using Core.System.Delta;
using Core.System.DocumentManagement;
using Core.System.DocumentManagement.Manager;
using Core.System.DocUSignJwt;
using Core.System.MicrosoftGraph;
using FluentValidation.AspNetCore;
using Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Royalty.Insurance.Api.Messaging;
using Royalty.Insurance.Api.Service;
using Royalty.Insurance.BusinessLayer;
using Royalty.Insurance.BusinessLayer.Common.Interfaces;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.BusinessLayer.Logic;
using Application.Interfaces;
using Royalty.Insurance.Settings;

namespace Royalty.Insurance.Api
{
    public class Startup
    {
        private readonly string _allowSpecificOrigins = "_allowSpecificOrigins";
        public Startup(IHostEnvironment env)
        {
            Console.WriteLine(env.EnvironmentName);
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(name: _allowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000")
                            .AllowAnyHeader()
                            .WithMethods("*")
                            .AllowCredentials();
                    });
            });
            services.AddControllers()
                .AddFluentValidation()
                .AddJsonOptions(options =>
                { 
                    options.JsonSerializerOptions.IgnoreNullValues = true;

                });
            services.AddApplicationInsightsTelemetry();
            //will be done before prod
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 443;
            //});
            //services.AddDbContext<RoyaltyInsuranceContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString(SystemConstants.DefaultConnection)));
            services.AddScoped<ITotpSetupGenerator, TotpSetupGenerator>();
            services.AddScoped<ITotpGenerator, TotpGenerator>();
            services.AddScoped<ITotpValidator, TotpValidator>();
            services.AddScoped<ITokenManager, TokenManager>();
            services.AddScoped<ITotpHelper, TotpHelper>();
            services.AddTransient<IUserIdProvider, UserIdProvider>();
            services.AddTransient<IRequestItemsService, RequestItemsService>();
            services.AddTransient<ClaimsPrincipal>(provider =>
            {
                var httpContext = provider.GetService<IHttpContextAccessor>().HttpContext;
                return httpContext?.User;
            });
            services.AddMapperService();
            services.AddSingleton<ICurrentUserService, CurrentUserService>();
            // Add functionality to inject IOptions<> T>
            // Add our Config object so it can be injected
            IConfiguration appSettingConfiguration = Configuration.GetSection(SystemConstants.AppSetting);
            services.Configure<AppSetting>(appSettingConfiguration);
            var appSetting = appSettingConfiguration.Get<AppSetting>();
            services.AddIntegrationServices(appSetting);
            services.AddInfrastructure(Configuration.GetConnectionString(SystemConstants.DefaultConnection));
            services.AddBusiness();
            services.AddDelta();
            services.AddMicrosoftGraph();
            services.AddDocumentManagement();
            services.AddSingleton<IOnlineLogic, OnlineLogic>();
            services.AddSingleton<UnreadTicker>();
            services.AddSignalR(options =>
            {
                options.MaximumReceiveMessageSize = 102400000;
                options.EnableDetailedErrors = true;
                options.KeepAliveInterval = TimeSpan.FromSeconds(10); //TODO investigate why it is killin on front
                options.AddFilter<GlobalFilter>();
            })
                //.AddMessagePackProtocol()
                ;
            services.Configure<IISServerOptions>(options => options.MaxRequestBodySize = 26214400);


            services.AddJwtAuthentication(appSetting.JwtTokenConfig);
            services.AddMemoryCache();
            services.AddDistributedMemoryCache();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(SystemConstants.ApiVersion, new OpenApiInfo
                {
                    Version = SystemConstants.ApiVersion,
                    Title = SystemConstants.SwaggerName,
                    Description = SystemConstants.SwaggerDescription
                });
                options.AddSecurityDefinition(SystemConstants.AuthenticationType, new OpenApiSecurityScheme
                {
                    Name = SystemConstants.Authorization,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = SystemConstants.AuthenticationType,
                    BearerFormat = SystemConstants.BearerFormat,
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Bearer {token}\"",
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = SystemConstants.AuthenticationType
                            }
                        },
                        new string[] {}

                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMiddleware<SignalRAuthorizationMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger(c =>
                {
                    c.SerializeAsV2 = true;
                });

            app.UseAuthentication();
            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(SystemConstants.SwaggerEndpoint, SystemConstants.SwaggerName);
                c.RoutePrefix = string.Empty;
            });
            app.UsePathBase("/api/v1");
            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors(_allowSpecificOrigins);

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<MessageHub>(MessageConstants.MessageHub);
                endpoints.MapControllers();
            });
        }
    }
}