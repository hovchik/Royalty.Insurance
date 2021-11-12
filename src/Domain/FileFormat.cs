using System.Collections.Generic;

namespace Domain
{
    public class FileFormat
    {
        public FileFormat()
        {
            UserGarages = new HashSet<UserGarage>();
        }

        
        public byte Id { get; set; }
        
        
        public string Name { get; set; }
        
        
        public string CodeType { get; set; }

        public ICollection<UserGarage> UserGarages { get; set; }
    }
}
