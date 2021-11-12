using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Domain;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Persistence
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<AchType> AchTypes { get; set; }
        public DbSet<AgaveSalesHistory> AgaveSalesHistories { get; set; }
        public DbSet<AgaveTransactionType> AgaveTransactionTypes { get; set; }
        public DbSet<Agency> Agencies { get; set; }
        public DbSet<AgentTask> AgentTasks { get; set; }
        public DbSet<AgentTaskStatus> AgentTaskStatuses { get; set; }
        public DbSet<AgentTaskType> AgentTaskTypes { get; set; }
        public DbSet<Attachment> Attachments { get; set; }
        public DbSet<BasicAlert> BasicAlerts { get; set; }
        public DbSet<CallType> CallTypes { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<CargoCommodity> CargoCommodities { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommonAuthType> CommonAuthTypes { get; set; }
        public DbSet<Coverage> Coverages { get; set; }
        public DbSet<CoverageType> CoverageTypes { get; set; }
        public DbSet<DatabaseVersion> DatabaseVersions { get; set; }
        public DbSet<Document> Documents { get; set; }
        public DbSet<DocumentType> DocumentTypes { get; set; }
        public DbSet<DriverInformation> DriverInformations { get; set; }
        public DbSet<FileFormat> FileFormats { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupMember> GroupMembers { get; set; }
        public DbSet<Gvwr> Gvwrs { get; set; }
        public DbSet<Insured> Insureds { get; set; }
        public DbSet<InsuredCoverage> InsuredCoverages { get; set; }
        public DbSet<InsuredStatus> InsuredStatuses { get; set; }
        public DbSet<InsuredVehicle> InsuredVehicles { get; set; }
        public DbSet<LegalStatus> LegalStatuses { get; set; }
        public DbSet<LocationType> LocationTypes { get; set; }
        public DbSet<LossInformation> LossInformations { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<MessageAttachment> MessageAttachments { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<OperationType> OperationTypes { get; set; }
        public DbSet<PhoneBook> PhoneBooks { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SavedMarketingRequest> SavedMarketingRequests { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<UnreadMessage> UnreadMessages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public DbSet<UserGarage> UserGarages { get; set; }
        public DbSet<UserPhone> UserPhones { get; set; }
        public DbSet<UserPhoneCallHistory> UserPhoneCallHistories { get; set; }
        public DbSet<UserStatus> UserStatuses { get; set; }
        public DbSet<UserTrustedDevice> UserTrustedDevices { get; set; }
        public DbSet<UsersProfile> UsersProfiles { get; set; }
        public DbSet<VehicleInfo> VehicleInfos { get; set; }
        public DbSet<ZipCode> ZipCodes { get; set; }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            var result = await base.SaveChangesAsync(cancellationToken);

            return result;
        }

        public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken)
        {
            return this.Database.BeginTransactionAsync(cancellationToken);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}
