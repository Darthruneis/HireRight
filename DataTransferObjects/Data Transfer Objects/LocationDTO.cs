using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class LocationDTO : DataTransferObjectBase
    {
        [DataMember]
        public AddressDTO Address { get; set; }

        [DataMember]
        public Guid CompanyGuid { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Label { get; set; }

        public LocationDTO()
        {
            Address = new AddressDTO();
        }
    }
}