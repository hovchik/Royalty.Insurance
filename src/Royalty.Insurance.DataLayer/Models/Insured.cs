using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class Insured
    {
        public Insured()
        {
            AgentTasks = new HashSet<AgentTask>();
            Cargos = new HashSet<Cargo>();
            Documents = new HashSet<Document>();
            DriverInformations = new HashSet<DriverInformation>();
            InsuredCoverages = new HashSet<InsuredCoverage>();
            InsuredVehicles = new HashSet<InsuredVehicle>();
            LossInformations = new HashSet<LossInformation>();
            UserGarages = new HashSet<UserGarage>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string SocialSecurityNumber { get; set; }
        [Required]
        [StringLength(50)]
        public string StateNumber { get; set; }
        [Required]
        [StringLength(120)]
        public string MotorCarrierNumber { get; set; }
        public bool IsFilings { get; set; }
        public int? InsuredStatusId { get; set; }
        public int YearsInsured { get; set; }
        public byte FartherState { get; set; }
        [Column("DBA")]
        [StringLength(100)]
        public string Dba { get; set; }
        public int MailingStateId { get; set; }
        public int MailingCityId { get; set; }
        public int MailingZipCodeId { get; set; }
        [Required]
        [StringLength(256)]
        public string MailingStreetAddress { get; set; }
        [StringLength(15)]
        public string MailingPhone { get; set; }
        [StringLength(256)]
        public string MailingEmail { get; set; }
        [Required]
        [StringLength(256)]
        public string MailingName { get; set; }
        public int GaragingStateId { get; set; }
        public int GaragingCityId { get; set; }
        public int GaragingZipCodeId { get; set; }
        [Required]
        [StringLength(256)]
        public string GaragingStreetAddress { get; set; }
        [StringLength(15)]
        public string GaragingPhone { get; set; }
        [StringLength(256)]
        public string GaragingEmail { get; set; }
        [Required]
        [StringLength(256)]
        public string GaragingName { get; set; }
        public int? LegalStatusId { get; set; }
        public int? DotNumber { get; set; }
        public int CreateBy { get; set; }
        public int UpdatedBy { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime CreateDatetimeUtc { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime LastModifiedUtc { get; set; }

        [ForeignKey(nameof(CreateBy))]
        [InverseProperty(nameof(User.InsuredCreateByNavigations))]
        public virtual User CreateByNavigation { get; set; }
        [ForeignKey(nameof(GaragingCityId))]
        [InverseProperty(nameof(City.InsuredGaragingCities))]
        public virtual City GaragingCity { get; set; }
        [ForeignKey(nameof(GaragingStateId))]
        [InverseProperty(nameof(State.InsuredGaragingStates))]
        public virtual State GaragingState { get; set; }
        [ForeignKey(nameof(GaragingZipCodeId))]
        [InverseProperty(nameof(ZipCode.InsuredGaragingZipCodes))]
        public virtual ZipCode GaragingZipCode { get; set; }
        [ForeignKey(nameof(InsuredStatusId))]
        [InverseProperty("Insureds")]
        public virtual InsuredStatus InsuredStatus { get; set; }
        [ForeignKey(nameof(LegalStatusId))]
        [InverseProperty("Insureds")]
        public virtual LegalStatus LegalStatus { get; set; }
        [ForeignKey(nameof(MailingCityId))]
        [InverseProperty(nameof(City.InsuredMailingCities))]
        public virtual City MailingCity { get; set; }
        [ForeignKey(nameof(MailingStateId))]
        [InverseProperty(nameof(State.InsuredMailingStates))]
        public virtual State MailingState { get; set; }
        [ForeignKey(nameof(MailingZipCodeId))]
        [InverseProperty(nameof(ZipCode.InsuredMailingZipCodes))]
        public virtual ZipCode MailingZipCode { get; set; }
        [ForeignKey(nameof(UpdatedBy))]
        [InverseProperty(nameof(User.InsuredUpdatedByNavigations))]
        public virtual User UpdatedByNavigation { get; set; }
        [InverseProperty(nameof(AgentTask.Insured))]
        public virtual ICollection<AgentTask> AgentTasks { get; set; }
        [InverseProperty(nameof(Cargo.Insured))]
        public virtual ICollection<Cargo> Cargos { get; set; }
        [InverseProperty(nameof(Document.Insured))]
        public virtual ICollection<Document> Documents { get; set; }
        [InverseProperty(nameof(DriverInformation.Insured))]
        public virtual ICollection<DriverInformation> DriverInformations { get; set; }
        [InverseProperty(nameof(InsuredCoverage.Insured))]
        public virtual ICollection<InsuredCoverage> InsuredCoverages { get; set; }
        [InverseProperty(nameof(InsuredVehicle.Insured))]
        public virtual ICollection<InsuredVehicle> InsuredVehicles { get; set; }
        [InverseProperty(nameof(LossInformation.Insured))]
        public virtual ICollection<LossInformation> LossInformations { get; set; }
        [InverseProperty(nameof(UserGarage.AssignedInsured))]
        public virtual ICollection<UserGarage> UserGarages { get; set; }
    }
}
