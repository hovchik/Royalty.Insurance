using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Index(nameof(Email), Name = "IX_Users", IsUnique = true)]
    public partial class User
    {
        public User()
        {
            AgaveSalesHistories = new HashSet<AgaveSalesHistory>();
            AgencyCreatedByNavigations = new HashSet<Agency>();
            AgencyUpdatedByNavigations = new HashSet<Agency>();
            AgentTaskAssignees = new HashSet<AgentTask>();
            AgentTaskCreatedByNavigations = new HashSet<AgentTask>();
            AgentTaskUpdatedByNavigations = new HashSet<AgentTask>();
            CargoCreateByNavigations = new HashSet<Cargo>();
            CargoUpdatedByNavigations = new HashSet<Cargo>();
            CommodityCreateByNavigations = new HashSet<Commodity>();
            CommodityUpdatedByNavigations = new HashSet<Commodity>();
            DocumentCreatedByNavigations = new HashSet<Document>();
            DocumentUpdatedByNavigations = new HashSet<Document>();
            DriverInformationCreatedByNavigations = new HashSet<DriverInformation>();
            DriverInformationUpdatedByNavigations = new HashSet<DriverInformation>();
            GroupCreatedByNavigations = new HashSet<Group>();
            GroupMembers = new HashSet<GroupMember>();
            GroupUpdatedByNavigations = new HashSet<Group>();
            InsuredCreateByNavigations = new HashSet<Insured>();
            InsuredUpdatedByNavigations = new HashSet<Insured>();
            Messages = new HashSet<Message>();
            PhoneBooks = new HashSet<PhoneBook>();
            SavedMarketingRequests = new HashSet<SavedMarketingRequest>();
            UnreadMessageReadUsers = new HashSet<UnreadMessage>();
            UnreadMessageSendUsers = new HashSet<UnreadMessage>();
            UserActivityLogs = new HashSet<UserActivityLog>();
            UserGarages = new HashSet<UserGarage>();
            UserPhoneCallHistories = new HashSet<UserPhoneCallHistory>();
            UserPhoneCreatedByNavigations = new HashSet<UserPhone>();
            UserPhonePhoneOwners = new HashSet<UserPhone>();
            UserPhoneUpdatedByNavigations = new HashSet<UserPhone>();
            UserTrustedDevices = new HashSet<UserTrustedDevice>();
            Notes = new HashSet<Note>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(50)]
        public string LastName { get; set; }
        [Required]
        [StringLength(256)]
        public string Email { get; set; }
        [MaxLength(64)]
        public byte[] Password { get; set; }
        public int Iteration { get; set; }
        [MaxLength(64)]
        public byte[] Salting { get; set; }
        public bool IsActive { get; set; }
        [StringLength(255)]
        public string PersonalAvatar { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ActivationExpiryDatetimeUtc { get; set; }
        [Required]
        public bool TemporaryPassword { get; set; }
        [StringLength(6)]
        public string ForgetPasswordCode { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? ForgetPasswordDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }
        [StringLength(15)]
        public string HomePhone { get; set; }
        [StringLength(15)]
        public string CellPhone { get; set; }
        [Required]
        [StringLength(15)]
        public string WorkPhone { get; set; }
        [StringLength(15)]
        public string AdditionalPhone { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int UserRoleId { get; set; }
        public int FailedLoginCount { get; set; }
        public bool IsBlocked { get; set; }

        [ForeignKey(nameof(UserRoleId))]
        [InverseProperty(nameof(Role.Users))]
        public virtual Role UserRole { get; set; }
        [InverseProperty("IdNavigation")]
        public virtual UsersProfile UsersProfile { get; set; }
        public virtual ICollection<AgaveSalesHistory> AgaveSalesHistories { get; set; }

        [InverseProperty(nameof(Agency.CreatedByNavigation))]
        public virtual ICollection<Agency> AgencyCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Agency.UpdatedByNavigation))]
        public virtual ICollection<Agency> AgencyUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(AgentTask.Assignee))]
        public virtual ICollection<AgentTask> AgentTaskAssignees { get; set; }
        [InverseProperty(nameof(AgentTask.CreatedByNavigation))]
        public virtual ICollection<AgentTask> AgentTaskCreatedByNavigations { get; set; }
        [InverseProperty(nameof(AgentTask.UpdatedByNavigation))]
        public virtual ICollection<AgentTask> AgentTaskUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Cargo.CreateByNavigation))]
        public virtual ICollection<Cargo> CargoCreateByNavigations { get; set; }
        [InverseProperty(nameof(Cargo.UpdatedByNavigation))]
        public virtual ICollection<Cargo> CargoUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Commodity.CreateByNavigation))]
        public virtual ICollection<Commodity> CommodityCreateByNavigations { get; set; }
        [InverseProperty(nameof(Commodity.UpdatedByNavigation))]
        public virtual ICollection<Commodity> CommodityUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Document.CreatedByNavigation))]
        public virtual ICollection<Document> DocumentCreatedByNavigations { get; set; }
        [InverseProperty(nameof(Document.UpdatedByNavigation))]
        public virtual ICollection<Document> DocumentUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(DriverInformation.CreatedByNavigation))]
        public virtual ICollection<DriverInformation> DriverInformationCreatedByNavigations { get; set; }
        [InverseProperty(nameof(DriverInformation.UpdatedByNavigation))]
        public virtual ICollection<DriverInformation> DriverInformationUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Group.CreatedByNavigation))]
        public virtual ICollection<Group> GroupCreatedByNavigations { get; set; }
        [InverseProperty(nameof(GroupMember.Member))]
        public virtual ICollection<GroupMember> GroupMembers { get; set; }
        [InverseProperty(nameof(Group.UpdatedByNavigation))]
        public virtual ICollection<Group> GroupUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Insured.CreateByNavigation))]
        public virtual ICollection<Insured> InsuredCreateByNavigations { get; set; }
        [InverseProperty(nameof(Insured.UpdatedByNavigation))]
        public virtual ICollection<Insured> InsuredUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(Message.Sender))]
        public virtual ICollection<Message> Messages { get; set; }
        [InverseProperty(nameof(PhoneBook.User))]
        public virtual ICollection<PhoneBook> PhoneBooks { get; set; }
        [InverseProperty(nameof(SavedMarketingRequest.User))]
        public virtual ICollection<SavedMarketingRequest> SavedMarketingRequests { get; set; }
        [InverseProperty(nameof(UnreadMessage.ReadUser))]
        public virtual ICollection<UnreadMessage> UnreadMessageReadUsers { get; set; }
        [InverseProperty(nameof(UnreadMessage.SendUser))]
        public virtual ICollection<UnreadMessage> UnreadMessageSendUsers { get; set; }
        [InverseProperty(nameof(UserActivityLog.User))]
        public virtual ICollection<UserActivityLog> UserActivityLogs { get; set; }
        [InverseProperty(nameof(UserGarage.User))]
        public virtual ICollection<UserGarage> UserGarages { get; set; }
        [InverseProperty(nameof(UserPhoneCallHistory.UserPhone))]
        public virtual ICollection<UserPhoneCallHistory> UserPhoneCallHistories { get; set; }
        [InverseProperty(nameof(UserPhone.CreatedByNavigation))]
        public virtual ICollection<UserPhone> UserPhoneCreatedByNavigations { get; set; }
        [InverseProperty(nameof(UserPhone.PhoneOwner))]
        public virtual ICollection<UserPhone> UserPhonePhoneOwners { get; set; }
        [InverseProperty(nameof(UserPhone.UpdatedByNavigation))]
        public virtual ICollection<UserPhone> UserPhoneUpdatedByNavigations { get; set; }
        [InverseProperty(nameof(UserTrustedDevice.User))]
        public virtual ICollection<UserTrustedDevice> UserTrustedDevices { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}
