using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class LocationDTO : DataTransferObjectBase
    {
        [DataMember]
        public AddressDTO Address { get; set; }

        [DataMember]
        public Guid CompanyId { get; set; }

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
            CompanyId = item.CompanyId;
            Description = item.Description;
            Label = item.Label;
        }
    }
}