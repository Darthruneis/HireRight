using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class CompanyDTO : DataTransferObjectBase
    {
        [DataMember]
        public AddressDTO BillingAddress { get; set; }

        [DataMember]
        public IList<ContactDTO> Contacts { get; set; }

        [DataMember]
        public IList<LocationDTO> Locations { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public IList<OrderDetailsDTO> Orders { get; set; }

        public CompanyDTO()
        {
            BillingAddress = new AddressDTO();
            Contacts = new List<ContactDTO>();
            Locations = new List<LocationDTO>();
            Orders = new List<OrderDetailsDTO>();
        }
    }
}