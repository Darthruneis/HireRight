using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class OrderDetailsDTO : DataTransferObjectBase
    {
        [DataMember]
        public Guid CompanyId { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public List<string> PositionsOfInterest { get; set; }

        [DataMember]
        public Guid ProductId { get; set; }

        [DataMember]
        public int Quantity { get; set; }
    }
}