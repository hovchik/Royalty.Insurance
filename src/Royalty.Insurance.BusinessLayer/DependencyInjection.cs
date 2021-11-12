using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Royalty.Insurance.BusinessLayer.Account;
using Royalty.Insurance.BusinessLayer.Agave;
using Royalty.Insurance.BusinessLayer.Agencies;
using Royalty.Insurance.BusinessLayer.AgentTasks;
using Royalty.Insurance.BusinessLayer.AgentTaskStatuses;
using Royalty.Insurance.BusinessLayer.Cab;
using Royalty.Insurance.BusinessLayer.Cargoes;
using Royalty.Insurance.BusinessLayer.Cities;
using Royalty.Insurance.BusinessLayer.Commodities;
using Royalty.Insurance.BusinessLayer.Common.Behaviours;
using Royalty.Insurance.BusinessLayer.Coverages;
using Royalty.Insurance.BusinessLayer.Documents;
using Royalty.Insurance.BusinessLayer.DriverInfo;
using Royalty.Insurance.BusinessLayer.Files;
using Royalty.Insurance.BusinessLayer.FlagmanWebHook;
using Royalty.Insurance.BusinessLayer.GroupMembers;
using Royalty.Insurance.BusinessLayer.Groups;
using Royalty.Insurance.BusinessLayer.ILogic;
using Royalty.Insurance.BusinessLayer.Insureds;
using Royalty.Insurance.BusinessLayer.Logic;
using Royalty.Insurance.BusinessLayer.LossInfo;
using Royalty.Insurance.BusinessLayer.Messages;
using Royalty.Insurance.BusinessLayer.Notes;
using Royalty.Insurance.BusinessLayer.PhoneBooks;
using Royalty.Insurance.BusinessLayer.Roles;
using Royalty.Insurance.BusinessLayer.SavedRequests;
using Royalty.Insurance.BusinessLayer.States.Queries.Mapper;
using Royalty.Insurance.BusinessLayer.UserPhoneSettings;
using Royalty.Insurance.BusinessLayer.Users;
using Royalty.Insurance.BusinessLayer.VehicleInfos;
using Royalty.Insurance.BusinessLayer.VinCheck;
using System.Common.EmailSender;
using System.Reflection;

namespace Royalty.Insurance.BusinessLayer
{
    public static class DependencyInjection
    {
        /// <summary>
        /// Inject of business logic
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddBusiness(this IServiceCollection services)
        {
            services.AddScoped<IAgencyMapperService, AgencyMapperService>();
            services.AddScoped<ICityMapperService, CityMapperService>();
            services.AddScoped<IStateMapperService, StateMapperService>();
            services.AddScoped<IDocumentMapperService, DocumentMapperService>();
            services.AddScoped<IUserGarageMapperService, UserGarageMapperService>();
            services.AddScoped<ICargoMapperService, CargoMapperService>();
            services.AddScoped<IDriverInfoMapperService, DriverInfoMapperService>();
            services.AddScoped<ILossInfoMapperService, LossInfoMapperService>();
            services.AddScoped<IMessageMapperService, MessageMapperService>();
            services.AddScoped<IGroupMapperService, GroupMapperService>();
            services.AddScoped<IGroupMemberMapperService, GroupMemberMapperService>();
            services.AddScoped<IAgaveSaleMapperService, AgaveSaleMapperService>();
            services.AddScoped<IPhoneBookMapperService, PhoneBookMapperService>();
            services.AddScoped<IVinMapperService, VinMapperService>();
            services.AddScoped<IAgentTaskMapperService, AgentTaskMapperService>();
            services.AddScoped<IUserPhoneMapperService, UserPhoneMapperService>();
            services.AddScoped<IUserPhoneCallHistoryMapperService, UserPhoneCallHistoryMapperService>();
            services.AddScoped<ISavedRequestMapperService, SavedRequestMapperService>();
            services.AddScoped<IVehicleInfoMapperService, VehicleInfoMapperService>();
            services.AddScoped<IRoleMapperService, RoleMapperService>();
            services.AddScoped<INoteMapperService, NoteMapperService>();
            services.AddScoped<IDotCoreMapperService, DotCoreMapperService>();
            services.AddScoped<IInsuredMapperService, InsuredMapperService>();
            services.AddScoped<IUserMapperService, UserMapperService>();
            services.AddScoped<IPersonalUserMapperService, PersonalUserMapperService>();
            services.AddScoped<IAdminUserMapperService, AdminUserMapperService>();
            services.AddScoped<IBaseUserProfileMapperService, BaseUserProfileMapperService>();
            services.AddScoped<IAgentTaskStatusMapperService, AgentTaskStatusMapperService>();
            services.AddScoped<ICommodityMapperService, CommodityMapperService>();
            services.AddScoped<ICoverageMapperService, CoverageMapperService>();
            services.AddScoped<IAccountMapperService, AccountMapperService>();

            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            services.AddScoped<IEmailSender, SmtpEmailSender/*EmailSender*/>();

            return services;
        }
    }
}
