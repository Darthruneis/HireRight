using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class OrderDetailsDTO : DataTransferObjectBase
    {
        [DataMember]
        public NotesPositionsDTO NotesAndPositions { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }

        public OrderDetailsDTO()
        {
            NotesAndPositions = new NotesPositionsDTO();
        }
    }
}