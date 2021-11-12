using System;
using System.Collections.Generic;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? InsuredId { get; set; }
        public DateTime CreateDateTime { get; set; }

        public virtual User User { get; set; }
    }
}
