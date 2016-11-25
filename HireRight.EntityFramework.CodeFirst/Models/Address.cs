using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    [ComplexType]
    public class Address
    {
        private string _zipCode;

        [Required]
        public string City { get; set; }

        [Required]
        public string Country { get; private set; }

        [Required, StringLength(10, MinimumLength = 5)]
        public string PostalCode
        {
            get { return _zipCode; }
            set
            {
                if (value.Length > 5)
                    if (value[5] != '-')
                        throw new ArgumentOutOfRangeException("PostalCode", value[5], "Postal Code must be in the format of \"XXXXX\" or \"XXXXX-XXXX\"");
                    else if (value.Length != 10)
                        throw new ArgumentOutOfRangeException("PostalCode.Length", value.Length, "Postal Code must be either 5 or 10 characters, in either the format of \"XXXXX\" or \"XXXXX-XXXX\"");

                _zipCode = value;
            }
        }

        [Required, StringLength(2, MinimumLength = 2)]
        public string State { get; set; }

        [Required]
        public string StreetAddress { get; set; }

        public string UnitNumber { get; set; }

        public Address()
        {
            Country = "United States";
        }

        public Address(string country)
        {
            Country = country;
        }
    }
}