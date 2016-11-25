using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Linq;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class ContactDTO : DataTransferObjectBase
    {
        [DataMember]
        public AddressDTO Address { get; set; }

        [DataMember]
        public string CellNumber { get; set; }

        [DataMember]
        public Guid CompanyId { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string FullName => FirstName + " " + LastName;

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsPrimary { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string OfficeNumber { get; set; }
    }
}