using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class RoyaltyInsuranceContext : DbContext
    {
        public RoyaltyInsuranceContext()
        {
        }

        public RoyaltyInsuranceContext(DbContextOptions<RoyaltyInsuranceContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AchType> AchTypes { get; set; }
        public virtual DbSet<AgaveSalesHistory> AgaveSalesHistories { get; set; }
        public virtual DbSet<AgaveTransactionType> AgaveTransactionTypes { get; set; }
        public virtual DbSet<Agency> Agencies { get; set; }
        public virtual DbSet<AgentTask> AgentTasks { get; set; }
        public virtual DbSet<AgentTaskStatus> AgentTaskStatuses { get; set; }
        public virtual DbSet<AgentTaskType> AgentTaskTypes { get; set; }
        public virtual DbSet<Attachment> Attachments { get; set; }
        public virtual DbSet<BasicAlert> BasicAlerts { get; set; }
        public virtual DbSet<CallType> CallTypes { get; set; }
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<CargoCommodity> CargoCommodities { get; set; }
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Commodity> Commodities { get; set; }
        public virtual DbSet<CommonAuthType> CommonAuthTypes { get; set; }
        public virtual DbSet<Coverage> Coverages { get; set; }
        public virtual DbSet<CoverageType> CoverageTypes { get; set; }
        public virtual DbSet<DatabaseVersion> DatabaseVersions { get; set; }
        public virtual DbSet<Document> Documents { get; set; }
        public virtual DbSet<DocumentType> DocumentTypes { get; set; }
        public virtual DbSet<DriverInformation> DriverInformations { get; set; }
        public virtual DbSet<FileFormat> FileFormats { get; set; }
        public virtual DbSet<Group> Groups { get; set; }
        public virtual DbSet<GroupMember> GroupMembers { get; set; }
        public virtual DbSet<Gvwr> Gvwrs { get; set; }
        public virtual DbSet<Insured> Insureds { get; set; }
        public virtual DbSet<InsuredCoverage> InsuredCoverages { get; set; }
        public virtual DbSet<InsuredStatus> InsuredStatuses { get; set; }
        public virtual DbSet<InsuredVehicle> InsuredVehicles { get; set; }
        public virtual DbSet<LegalStatus> LegalStatuses { get; set; }
        public virtual DbSet<LocationType> LocationTypes { get; set; }
        public virtual DbSet<LossInformation> LossInformations { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<MessageAttachment> MessageAttachments { get; set; }
        public virtual DbSet<Note> Notes { get; set; }
        public virtual DbSet<OperationType> OperationTypes { get; set; }
        public virtual DbSet<PhoneBook> PhoneBooks { get; set; }
        public virtual DbSet<Role> Roles { get; set; }
        public virtual DbSet<SavedMarketingRequest> SavedMarketingRequests { get; set; }
        public virtual DbSet<State> States { get; set; }
        public virtual DbSet<UnreadMessage> UnreadMessages { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UserActivityLog> UserActivityLogs { get; set; }
        public virtual DbSet<UserGarage> UserGarages { get; set; }
        public virtual DbSet<UserPhone> UserPhones { get; set; }
        public virtual DbSet<UserPhoneCallHistory> UserPhoneCallHistories { get; set; }
        public virtual DbSet<UserStatus> UserStatuses { get; set; }
        public virtual DbSet<UserTrustedDevice> UserTrustedDevices { get; set; }
        public virtual DbSet<UsersProfile> UsersProfiles { get; set; }
        public virtual DbSet<VehicleInfo> VehicleInfos { get; set; }
        public virtual DbSet<ZipCode> ZipCodes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.LogTo(Console.WriteLine);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            

            modelBuilder.Entity<AgaveSalesHistory>(entity =>
            {
                entity.Property(e => e.CreateDateTimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.TransactionType)
                    .WithMany(p => p.AgaveSalesHistories)
                    .HasForeignKey(d => d.TransactionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgaveSalesHistory_AgaveTransactionType");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AgaveSalesHistories)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgaveSalesHistory_Users");
            });

            modelBuilder.Entity<Agency>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.FaxNumber).IsUnicode(false);

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.PhoneNumber).IsUnicode(false);

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AgencyCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agencies_CreatedByUsers");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AgencyUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Agencies_UpdatedByUsers");
            });

            modelBuilder.Entity<AgentTask>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.AgentTaskStatus)
                    .WithMany(p => p.AgentTasks)
                    .HasForeignKey(d => d.AgentTaskStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentTasks_AgentTaskStatuses");

                entity.HasOne(d => d.AgentTaskType)
                    .WithMany(p => p.AgentTasks)
                    .HasForeignKey(d => d.AgentTaskTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentTasks_AgentTaskTypes");

                entity.HasOne(d => d.Assignee)
                    .WithMany(p => p.AgentTaskAssignees)
                    .HasForeignKey(d => d.AssigneeId)
                    .HasConstraintName("FK_AgentTasks_Users2");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.AgentTaskCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentTasks_Users");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.AgentTasks)
                    .HasForeignKey(d => d.InsuredId)
                    .HasConstraintName("FK_AgentTasks_Insureds");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.AgentTaskUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_AgentTasks_Users1");
            });

            modelBuilder.Entity<AgentTaskStatus>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");
            });

            modelBuilder.Entity<Attachment>(entity =>
            {
                entity.HasOne(d => d.UserGarage)
                    .WithMany(p => p.Attachments)
                    .HasForeignKey(d => d.UserGarageId)
                    .HasConstraintName("FK_Attachments_UserGarages");
            });

            modelBuilder.Entity<Cargo>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.CargoCreateByNavigations)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cargo_Users");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.Cargos)
                    .HasForeignKey(d => d.InsuredId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cargo_Insureds");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CargoUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cargo_Users1");
            });

            modelBuilder.Entity<CargoCommodity>(entity =>
            {
                entity.HasOne(d => d.Cargo)
                    .WithMany(p => p.CargoCommodities)
                    .HasForeignKey(d => d.CargoId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CargoCommodity_Cargo");

                entity.HasOne(d => d.Commodity)
                    .WithMany(p => p.CargoCommodities)
                    .HasForeignKey(d => d.CommodityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CargoCommodity_Commodity");
            });

            modelBuilder.Entity<City>(entity =>
            {
                entity.HasOne(d => d.State)
                    .WithMany(p => p.Cities)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Cities_States");
            });

            modelBuilder.Entity<Commodity>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Name).HasDefaultValueSql("('A')");

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.CommodityCreateByNavigations)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commodity_Users");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.CommodityUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Commodity_Users1");
            });

            modelBuilder.Entity<DatabaseVersion>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.DbVersion)
                    .IsUnicode(false)
                    .HasDefaultValueSql("(CONVERT([varchar],getutcdate(),(4)))");
            });

            modelBuilder.Entity<Document>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DocumentCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_Users");

                entity.HasOne(d => d.DocumentType)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.DocumentTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_DocumentType");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.InsuredId)
                    .HasConstraintName("FK_Documents_Insureds");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DocumentUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_Users1");
            });

            modelBuilder.Entity<DriverInformation>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.DriverInformationCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverInformation_Users");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.DriverInformations)
                    .HasForeignKey(d => d.InsuredId)
                    .HasConstraintName("FK_DriverInformation_Insureds");

                entity.HasOne(d => d.State)
                    .WithMany(p => p.DriverInformations)
                    .HasForeignKey(d => d.StateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverInformation_States");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.DriverInformationUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DriverInformation_Users1");
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.GroupCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_Users");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.GroupUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Groups_UpdatedBy");
            });

            modelBuilder.Entity<GroupMember>(entity =>
            {
                entity.Property(e => e.Active).HasDefaultValueSql("((1))");

                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMembers_Groups");

                entity.HasOne(d => d.Member)
                    .WithMany(p => p.GroupMembers)
                    .HasForeignKey(d => d.MemberId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_GroupMembers_Members");
            });

            modelBuilder.Entity<Insured>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.GaragingPhone).IsUnicode(false);

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.MailingPhone).IsUnicode(false);

                entity.Property(e => e.SocialSecurityNumber).IsUnicode(false);

                entity.HasOne(d => d.CreateByNavigation)
                    .WithMany(p => p.InsuredCreateByNavigations)
                    .HasForeignKey(d => d.CreateBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_CreateByUsers");

                entity.HasOne(d => d.GaragingCity)
                    .WithMany(p => p.InsuredGaragingCities)
                    .HasForeignKey(d => d.GaragingCityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_GaragingCities");

                entity.HasOne(d => d.GaragingState)
                    .WithMany(p => p.InsuredGaragingStates)
                    .HasForeignKey(d => d.GaragingStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_GaragingStates");

                entity.HasOne(d => d.GaragingZipCode)
                    .WithMany(p => p.InsuredGaragingZipCodes)
                    .HasForeignKey(d => d.GaragingZipCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_GaragingZipCode");

                entity.HasOne(d => d.InsuredStatus)
                    .WithMany(p => p.Insureds)
                    .HasForeignKey(d => d.InsuredStatusId)
                    .HasConstraintName("FK_Insureds_InsuredStatuses");

                entity.HasOne(d => d.LegalStatus)
                    .WithMany(p => p.Insureds)
                    .HasForeignKey(d => d.LegalStatusId)
                    .HasConstraintName("FK_Insureds_LegalStatuses");

                entity.HasOne(d => d.MailingCity)
                    .WithMany(p => p.InsuredMailingCities)
                    .HasForeignKey(d => d.MailingCityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_MailingCities");

                entity.HasOne(d => d.MailingState)
                    .WithMany(p => p.InsuredMailingStates)
                    .HasForeignKey(d => d.MailingStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_MailingStates");

                entity.HasOne(d => d.MailingZipCode)
                    .WithMany(p => p.InsuredMailingZipCodes)
                    .HasForeignKey(d => d.MailingZipCodeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_MailingZipCode");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.InsuredUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Insureds_UpdatedByUsers");
            });

            modelBuilder.Entity<InsuredCoverage>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Coverage)
                    .WithMany(p => p.InsuredCoverages)
                    .HasForeignKey(d => d.CoverageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredCoverages_Coverages");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.InsuredCoverages)
                    .HasForeignKey(d => d.InsuredId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredCoverages_Insureds");
            });

            modelBuilder.Entity<InsuredVehicle>(entity =>
            {
                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.InsuredVehicles)
                    .HasForeignKey(d => d.InsuredId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredVehicle_Insureds");

                entity.HasOne(d => d.Vehicle)
                    .WithMany(p => p.InsuredVehicles)
                    .HasForeignKey(d => d.VehicleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InsuredVehicle_VehicleInfo");
            });

            modelBuilder.Entity<LossInformation>(entity =>
            {
                entity.Property(e => e.EffectiveDate).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ExpireDate).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Insured)
                    .WithMany(p => p.LossInformations)
                    .HasForeignKey(d => d.InsuredId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_LossInformation_Insureds");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Messages_Messages");

                entity.HasOne(d => d.RecipientGroup)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.RecipientGroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Groups");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Messages_Users");
            });

            modelBuilder.Entity<MessageAttachment>(entity =>
            {
                entity.HasKey(e => new { e.MessageId, e.AttachmentId })
                    .HasName("PK_MessageAttachments_1");

                entity.HasOne(d => d.Attachment)
                    .WithMany(p => p.MessageAttachments)
                    .HasForeignKey(d => d.AttachmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageAttachments_Attachments");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.MessageAttachments)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MessageAttachments_Messages");
            });

            modelBuilder.Entity<Note>(entity =>
            {
                entity.Property(e => e.CreateDateTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(256);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notes)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notes_Users");
            });
            modelBuilder.Entity<PhoneBook>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.PhoneBooks)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PhoneBook_Users");
            });

            modelBuilder.Entity<SavedMarketingRequest>(entity =>
            {
                entity.Property(e => e.CreatedDateUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.SavedMarketingRequests)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SavedMarketingRequests_Users");
            });

            modelBuilder.Entity<UnreadMessage>(entity =>
            {
                entity.Property(e => e.ReadDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.Group)
                    .WithMany(p => p.UnreadMessages)
                    .HasForeignKey(d => d.GroupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnReadMessages_Group");

                entity.HasOne(d => d.Message)
                    .WithMany(p => p.UnreadMessages)
                    .HasForeignKey(d => d.MessageId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnReadMessages_Messages");

                entity.HasOne(d => d.ReadUser)
                    .WithMany(p => p.UnreadMessageReadUsers)
                    .HasForeignKey(d => d.ReadUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnReadMessages_Users");

                entity.HasOne(d => d.SendUser)
                    .WithMany(p => p.UnreadMessageSendUsers)
                    .HasForeignKey(d => d.SendUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UnReadMessages_SendUsers");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.AdditionalPhone).IsUnicode(false);

                entity.Property(e => e.CellPhone).IsUnicode(false);

                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.ForgetPasswordCode).IsUnicode(false);

                entity.Property(e => e.HomePhone).IsUnicode(false);

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.Password).IsFixedLength(true);

                entity.Property(e => e.Salting).IsFixedLength(true);

                entity.Property(e => e.TemporaryPassword).HasDefaultValueSql("((1))");

                entity.Property(e => e.WorkPhone).IsUnicode(false);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.UserRoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Users_Roles");
            });

            modelBuilder.Entity<UserActivityLog>(entity =>
            {
                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserActivityLogs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserActivityLogs_Users");
            });

            modelBuilder.Entity<UserGarage>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.AssignedInsured)
                    .WithMany(p => p.UserGarages)
                    .HasForeignKey(d => d.AssignedInsuredId)
                    .HasConstraintName("FK_UserGarages_Insureds");

                entity.HasOne(d => d.FileFormat)
                    .WithMany(p => p.UserGarages)
                    .HasForeignKey(d => d.FileFormatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGarages_FileFormats");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserGarages)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserGarages_Users");
            });

            modelBuilder.Entity<UserPhone>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.Property(e => e.LastModifiedUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.UserPhoneCreatedByNavigations)
                    .HasForeignKey(d => d.CreatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhones_UsersCreated");

                entity.HasOne(d => d.PhoneOwner)
                    .WithMany(p => p.UserPhonePhoneOwners)
                    .HasForeignKey(d => d.PhoneOwnerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhones_UsersOwner");

                entity.HasOne(d => d.UpdatedByNavigation)
                    .WithMany(p => p.UserPhoneUpdatedByNavigations)
                    .HasForeignKey(d => d.UpdatedBy)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhones_UsersUpdated");
            });

            modelBuilder.Entity<UserPhoneCallHistory>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.CurrentCallType)
                    .WithMany(p => p.UserPhoneCallHistoryCurrentCallTypes)
                    .HasForeignKey(d => d.CurrentCallTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhoneCallHistory_UserPhoneCallHistory");

                entity.HasOne(d => d.InitialCallType)
                    .WithMany(p => p.UserPhoneCallHistoryInitialCallTypes)
                    .HasForeignKey(d => d.InitialCallTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhoneCallHistory_CallTypes");

                entity.HasOne(d => d.UserPhone)
                    .WithMany(p => p.UserPhoneCallHistories)
                    .HasForeignKey(d => d.UserPhoneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserPhoneCallHistory_Users");
            });

            modelBuilder.Entity<UserStatus>(entity =>
            {
                entity.Property(e => e.Name).IsUnicode(false);
            });

            modelBuilder.Entity<UserTrustedDevice>(entity =>
            {
                entity.Property(e => e.CreateDatetimeUtc).HasDefaultValueSql("(getutcdate())");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserTrustedDevices)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UserTrustedDevices_Users");
            });

            modelBuilder.Entity<UsersProfile>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.UserLastStatusId).HasDefaultValueSql("((2))");

                entity.HasOne(d => d.IdNavigation)
                    .WithOne(p => p.UsersProfile)
                    .HasForeignKey<UsersProfile>(d => d.Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersProfiles_Users");

                entity.HasOne(d => d.UserLastStatus)
                    .WithMany(p => p.UsersProfileUserLastStatuses)
                    .HasForeignKey(d => d.UserLastStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersProfiles_UserStatus1");

                entity.HasOne(d => d.UserStatus)
                    .WithMany(p => p.UsersProfileUserStatuses)
                    .HasForeignKey(d => d.UserStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_UsersProfiles_UserStatus");
            });

            modelBuilder.Entity<ZipCode>(entity =>
            {
                entity.HasOne(d => d.City)
                    .WithMany(p => p.ZipCodes)
                    .HasForeignKey(d => d.CityId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ZipCode_Cities");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
