using System;
using System.Collections.Generic;

#nullable disable

namespace Royalty.Insurance.DataLayer.Models
{
    public partial class AgaveTransactionType
    {
        public AgaveTransactionType()
        {
            AgaveSalesHistories = new HashSet<AgaveSalesHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AgaveSalesHistory> AgaveSalesHistories { get; set; }
    }
}
