using System.Collections.Generic;
namespace Domain
{
    public class AgaveTransactionType
    {
        public AgaveTransactionType()
        {
            AgaveSalesHistories = new HashSet<AgaveSalesHistory>();
        }

        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<AgaveSalesHistory> AgaveSalesHistories { get; set; }
    }
}
