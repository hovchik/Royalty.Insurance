using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class FileFormat
    {
        public FileFormat()
        {
            UserGarages = new HashSet<UserGarage>();
        }

        [Key]
        public byte Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        [StringLength(50)]
        public string CodeType { get; set; }

        [InverseProperty(nameof(UserGarage.FileFormat))]
        public virtual ICollection<UserGarage> UserGarages { get; set; }
    }
}
