using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

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

        public string CreateQuery(string propertyName)
        {
            string prefix = $"&filter.{propertyName}.";
            StringBuilder query = new StringBuilder("");

            if (!string.IsNullOrWhiteSpace(City))
                query.Append(prefix + $"{nameof(City)}={City}");

            if (!string.IsNullOrWhiteSpace(Country))
                query.Append(prefix + $"{nameof(Country)}={Country}");

            if (!string.IsNullOrWhiteSpace(PostalCode))
                query.Append(prefix + $"{nameof(PostalCode)}={PostalCode}");

            if (!string.IsNullOrWhiteSpace(State))
                query.Append(prefix + $"{nameof(State)}={State}");

            if (!string.IsNullOrWhiteSpace(StreetAddress))
                query.Append(prefix + $"{nameof(StreetAddress)}={StreetAddress}");

            if (!string.IsNullOrWhiteSpace(UnitNumber))
                query.Append(prefix + $"{nameof(UnitNumber)}={UnitNumber}");

            return query.Append("}").ToString();
        }
    }
}