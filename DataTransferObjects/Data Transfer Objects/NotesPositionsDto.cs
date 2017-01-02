using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    public class NotesPositionsDTO
    {
        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public List<string> PositionsOfInterest { get; set; }

        public NotesPositionsDTO()
        {
            PositionsOfInterest = new List<string>();
        }
    }
}