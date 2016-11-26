using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DataTransferObjects.Data_Transfer_Objects
{
    [Serializable]
    public class AddressDTO
    {
        [DataMember, Required]
        public string City { get; set; }

        [DataMember, Required]
        public string Country { get; set; }

        public string GetFullAddress =>
            $"{StreetAddress} {UnitNumber}, {City}, {State} {PostalCode}";

        [DataMember, Required, Display(Name = "ZIP or Postal Code")]
        [StringLength(10, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [DataMember, Required, Display(Name = "State or Province")]
        public string State { get; set; }

        [DataMember, Required]
        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        [DataMember]
        [Display(Name = "Apt, Suite, Unit")]
        public string UnitNumber { get; set; }

        public AddressDTO(Address address)
        {
            City = address.City;
            Country = address.Country;
            PostalCode = address.PostalCode;
            State = address.State;
            StreetAddress = address.StreetAddress;
            UnitNumber = address.UnitNumber;
        }

        public AddressDTO()
        {
            Country = "United States";
        }
    }
}