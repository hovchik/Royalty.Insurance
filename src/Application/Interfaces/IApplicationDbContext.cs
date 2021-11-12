using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace Application.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<AchType> AchTypes { get; set; }
        DbSet<AgaveSalesHistory> AgaveSalesHistories { get; set; }
        DbSet<AgaveTransactionType> AgaveTransactionTypes { get; set; }
        DbSet<Agency> Agencies { get; set; }

        DbSet<AgentTask> AgentTasks { get; set; }

        DbSet<AgentTaskStatus> AgentTaskStatuses { get; set; }
        DbSet<AgentTaskType> AgentTaskTypes { get; set; }
        DbSet<Attachment> Attachments { get; set; }
        DbSet<BasicAlert> BasicAlerts { get; set; }
        DbSet<CallType> CallTypes { get; set; }
        DbSet<Cargo> Cargos { get; set; }
        DbSet<CargoCommodity> CargoCommodities { get; set; }
        DbSet<City> Cities { get; set; }
        DbSet<Commodity> Commodities { get; set; }
        DbSet<CommonAuthType> CommonAuthTypes { get; set; }
        DbSet<Coverage> Coverages { get; set; }
        DbSet<CoverageType> CoverageTypes { get; set; }
        DbSet<DatabaseVersion> DatabaseVersions { get; set; }
        DbSet<Document> Documents { get; set; }
        DbSet<DocumentType> DocumentTypes { get; set; }
        DbSet<DriverInformation> DriverInformations { get; set; }
        DbSet<FileFormat> FileFormats { get; set; }
        DbSet<Group> Groups { get; set; }
        DbSet<GroupMember> GroupMembers { get; set; }
        DbSet<Gvwr> Gvwrs { get; set; }
        DbSet<Insured> Insureds { get; set; }
        DbSet<InsuredCoverage> InsuredCoverages { get; set; }
        DbSet<InsuredStatus> InsuredStatuses { get; set; }
        DbSet<InsuredVehicle> InsuredVehicles { get; set; }
        DbSet<LegalStatus> LegalStatuses { get; set; }
        DbSet<LocationType> LocationTypes { get; set; }
        DbSet<LossInformation> LossInformations { get; set; }
        DbSet<Message> Messages { get; set; }
        DbSet<MessageAttachment> MessageAttachments { get; set; }
        DbSet<Note> Notes { get; set; }
        DbSet<OperationType> OperationTypes { get; set; }
        DbSet<PhoneBook> PhoneBooks { get; set; }
        DbSet<Role> Roles { get; set; }
        DbSet<SavedMarketingRequest> SavedMarketingRequests { get; set; }
        DbSet<State> States { get; set; }
        DbSet<UnreadMessage> UnreadMessages { get; set; }
        DbSet<User> Users { get; set; }
        DbSet<UserActivityLog> UserActivityLogs { get; set; }
        DbSet<UserGarage> UserGarages { get; set; }
        DbSet<UserPhone> UserPhones { get; set; }
        DbSet<UserPhoneCallHistory> UserPhoneCallHistories { get; set; }
        DbSet<UserStatus> UserStatuses { get; set; }
        DbSet<UserTrustedDevice> UserTrustedDevices { get; set; }
        DbSet<UsersProfile> UsersProfiles { get; set; }
        DbSet<VehicleInfo> VehicleInfos { get; set; }
        DbSet<ZipCode> ZipCodes { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);

        Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken);
    }
}