using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class State
    {
        public State()
        {
            Cities = new HashSet<City>();
            DriverInformations = new HashSet<DriverInformation>();
            InsuredGaragingStates = new HashSet<Insured>();
            InsuredMailingStates = new HashSet<Insured>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }

        [InverseProperty(nameof(City.State))]
        public virtual ICollection<City> Cities { get; set; }
        [InverseProperty(nameof(DriverInformation.State))]
        public virtual ICollection<DriverInformation> DriverInformations { get; set; }
        [InverseProperty(nameof(Insured.GaragingState))]
        public virtual ICollection<Insured> InsuredGaragingStates { get; set; }
        [InverseProperty(nameof(Insured.MailingState))]
        public virtual ICollection<Insured> InsuredMailingStates { get; set; }
    }
}
