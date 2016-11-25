using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [DataContract]
    public class DiscountDTO : DataTransferObjectBase
    {
        [DataMember(EmitDefaultValue = false)]
        public decimal Amount { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public bool IsPercent { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid ProductId { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int Threshold { get; set; }
    }
}