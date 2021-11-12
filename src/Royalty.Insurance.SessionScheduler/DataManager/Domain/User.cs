using System;
using System.Collections.Generic;

namespace Royalty.Insurance.SessionScheduler.DataManager.Domain
{
    public class User
    {

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

        public virtual UsersProfile UsersProfile { get; set; }
        
    }
}
