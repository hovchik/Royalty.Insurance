using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    [Table("ZipCode")]
    public partial class ZipCode
    {
        public ZipCode()
        {
            InsuredGaragingZipCodes = new HashSet<Insured>();
            InsuredMailingZipCodes = new HashSet<Insured>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(16)]
        public string Code { get; set; }
        public int CityId { get; set; }

        [ForeignKey(nameof(CityId))]
        [InverseProperty("ZipCodes")]
        public virtual City City { get; set; }
        [InverseProperty(nameof(Insured.GaragingZipCode))]
        public virtual ICollection<Insured> InsuredGaragingZipCodes { get; set; }
        [InverseProperty(nameof(Insured.MailingZipCode))]
        public virtual ICollection<Insured> InsuredMailingZipCodes { get; set; }
    }
}
