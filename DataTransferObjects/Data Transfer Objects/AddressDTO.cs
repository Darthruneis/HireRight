using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

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

        [DataMember, Required, Display(Name = "ZIP")]
        [StringLength(10, MinimumLength = 5)]
        public string PostalCode { get; set; }

        [DataMember, Required, Display(Name = "State")]
        [StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [DataMember, Required]
        [Display(Name = "Address")]
        public string StreetAddress { get; set; }

        [DataMember]
        [Display(Name = "Unit")]
        public string UnitNumber { get; set; }

        public AddressDTO()
        {
            Country = "United States";
        }
    }
}