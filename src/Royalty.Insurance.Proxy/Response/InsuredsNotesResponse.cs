using Domain;
using System.Collections.Generic;

namespace Royalty.Insurance.Proxy.Response
{
    public class InsuredsNotesResponse
    {
        public int InsuredId { get; set; }
        public string InsuredName { get; set; }
        public List<NoteResponse> AllNotes { get; set; }
    }
}
