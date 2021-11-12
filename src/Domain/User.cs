using System;
using System.Collections.Generic;

namespace Domain
{
    public class User
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

        
        public int Id { get; set; }
        
        
        public string FirstName { get; set; }
        
        
        public string LastName { get; set; }
        
        public string Email { get; set; }
        public byte[] Password { get; set; }
        public int Iteration { get; set; }
        public byte[] Salting { get; set; }
        public bool IsActive { get; set; }
        
        public string PersonalAvatar { get; set; }
        
        public DateTime? ActivationExpiryDatetimeUtc { get; set; }
        
        public bool TemporaryPassword { get; set; }
        public string ForgetPasswordCode { get; set; }
        
        public DateTime? ForgetPasswordDatetimeUtc { get; set; }
        
        public DateTime CreateDatetimeUtc { get; set; }
        
        public DateTime LastModifiedUtc { get; set; }
        
        public string HomePhone { get; set; }
        
        public string CellPhone { get; set; }
        
        public string WorkPhone { get; set; }
        
        public string AdditionalPhone { get; set; }
        public bool TwoFactorEnabled { get; set; }
        public int UserRoleId { get; set; }
        public int FailedLoginCount { get; set; }
        public bool IsBlocked { get; set; }

        public Role UserRole { get; set; }
        public UsersProfile UsersProfile { get; set; }
        public ICollection<AgaveSalesHistory> AgaveSalesHistories { get; set; }
        public ICollection<Agency> AgencyCreatedByNavigations { get; set; }
        public ICollection<Agency> AgencyUpdatedByNavigations { get; set; }
        public ICollection<AgentTask> AgentTaskAssignees { get; set; }
        public ICollection<AgentTask> AgentTaskCreatedByNavigations { get; set; }
        public ICollection<AgentTask> AgentTaskUpdatedByNavigations { get; set; }
        public ICollection<Cargo> CargoCreateByNavigations { get; set; }
        public ICollection<Cargo> CargoUpdatedByNavigations { get; set; }
        public ICollection<Commodity> CommodityCreateByNavigations { get; set; }
        public ICollection<Commodity> CommodityUpdatedByNavigations { get; set; }
        public ICollection<Document> DocumentCreatedByNavigations { get; set; }
        public ICollection<Document> DocumentUpdatedByNavigations { get; set; }
        public ICollection<DriverInformation> DriverInformationCreatedByNavigations { get; set; }
        public ICollection<DriverInformation> DriverInformationUpdatedByNavigations { get; set; }
        public ICollection<Group> GroupCreatedByNavigations { get; set; }
        public ICollection<GroupMember> GroupMembers { get; set; }
        public ICollection<Group> GroupUpdatedByNavigations { get; set; }
        public ICollection<Insured> InsuredCreateByNavigations { get; set; }
        public ICollection<Insured> InsuredUpdatedByNavigations { get; set; }
        public ICollection<Message> Messages { get; set; }
        public ICollection<PhoneBook> PhoneBooks { get; set; }
        public ICollection<SavedMarketingRequest> SavedMarketingRequests { get; set; }
        public ICollection<UnreadMessage> UnreadMessageReadUsers { get; set; }
        public ICollection<UnreadMessage> UnreadMessageSendUsers { get; set; }
        public ICollection<UserActivityLog> UserActivityLogs { get; set; }
        public ICollection<UserGarage> UserGarages { get; set; }
        public ICollection<UserPhoneCallHistory> UserPhoneCallHistories { get; set; }
        public ICollection<UserPhone> UserPhoneCreatedByNavigations { get; set; }
        public ICollection<UserPhone> UserPhonePhoneOwners { get; set; }
        public ICollection<UserPhone> UserPhoneUpdatedByNavigations { get; set; }
        public ICollection<UserTrustedDevice> UserTrustedDevices { get; set; }

        public ICollection<Note> Notes { get; set; }
    }
}
