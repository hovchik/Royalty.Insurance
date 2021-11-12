using System;

namespace Domain
{
    public class Note
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public int? InsuredId { get; set; }
        public DateTime CreateDateTime { get; set; }

        public User User { get; set; }
    }
}
