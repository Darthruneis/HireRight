using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class NewOrderDTO : DataTransferObjectBase
    {
        [DataMember]
        public CompanyDTO Company { get; set; }

        [DataMember]
        public OrderDetailsDTO Order { get; set; }

        [DataMember]
        public ContactDTO PrimaryContact { get; set; }

        [DataMember]
        public ContactDTO SecondaryContact { get; set; }

        public NewOrderDTO()
        {
            Company = new CompanyDTO();
            Order = new OrderDetailsDTO();
            PrimaryContact = new ContactDTO();
            SecondaryContact = new ContactDTO();
        }
    }
}