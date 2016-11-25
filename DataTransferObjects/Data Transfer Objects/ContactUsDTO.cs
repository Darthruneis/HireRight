using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class ContactUsDTO : ContactDTO
    {
        [DataMember]
        public new AddressDTO Address { get; set; }

        [DataMember]
        public string CellPhone { get; set; }

        [DataMember]
        public string Message { get; set; }

        [DataMember]
        public string OfficePhone { get; set; }

        [DataMember]
        public string State { get; set; }

        [DataMember]
        public string ZIP { get; set; }
    }
}