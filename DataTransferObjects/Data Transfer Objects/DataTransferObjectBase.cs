using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [DataContract]
    public abstract class DataTransferObjectBase
    {
        [DataMember(EmitDefaultValue = false)]
        public DateTime CreatedUtc { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid RowGuid { get; set; }
    }
}