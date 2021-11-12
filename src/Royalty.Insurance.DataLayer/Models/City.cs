using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class City
    {
        public City()
        {
            InsuredGaragingCities = new HashSet<Insured>();
            InsuredMailingCities = new HashSet<Insured>();
            ZipCodes = new HashSet<ZipCode>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(64)]
        public string Name { get; set; }
        public int StateId { get; set; }

        [ForeignKey(nameof(StateId))]
        [InverseProperty("Cities")]
        public virtual State State { get; set; }
        [InverseProperty(nameof(Insured.GaragingCity))]
        public virtual ICollection<Insured> InsuredGaragingCities { get; set; }
        [InverseProperty(nameof(Insured.MailingCity))]
        public virtual ICollection<Insured> InsuredMailingCities { get; set; }
        [InverseProperty(nameof(ZipCode.City))]
        public virtual ICollection<ZipCode> ZipCodes { get; set; }
    }
}
