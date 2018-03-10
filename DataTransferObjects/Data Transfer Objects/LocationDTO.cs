using System;
using System.Runtime.Serialization;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

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

        public LocationDTO(CompanyLocation item)
        {
            Address = new AddressDTO(item.Address);
            CompanyGuid = item.Company.RowGuid;
            Description = item.Description;
            Label = item.Label;
        }
    }
}