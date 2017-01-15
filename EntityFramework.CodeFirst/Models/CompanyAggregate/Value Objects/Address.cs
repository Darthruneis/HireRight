using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// The address of a company or contact. This is a value object.
    /// </summary>
    [ComplexType]
    public class Address
    {
        /// <summary>
        /// The city for this address.
        /// </summary>
        [Required, Column("City")]
        public string City { get; private set; }

        /// <summary>
        /// The country this address resides in.
        /// </summary>
        [Required, Column("Country")]
        public string Country { get; private set; }

        /// <summary>
        /// The postal code of this address.
        /// </summary>
        [Required, StringLength(10, MinimumLength = 5), Column("PostalCode")]
        public string PostalCode { get; private set; }

        /// <summary>
        /// The state or province of this address.
        /// </summary>
        [Required, StringLength(2, MinimumLength = 2), Column("State")]
        public string State { get; private set; }

        /// <summary>
        /// The street and street number of this address/
        /// </summary>
        [Required, Column("StreetAddress")]
        public string StreetAddress { get; private set; }

        /// <summary>
        /// Optional. If there is an assosciated unit with the address, such as an appartment or room number, include it here.
        /// </summary>
        [Column("UnitNumber")]
        public string UnitNumber { get; private set; }

        /// <summary>
        /// Creates a new <see cref="Address" /> in the United States.
        /// </summary>
        /// <param name="street">The street address for this <see cref="Address" />.</param>
        /// <param name="city">  The city of this <see cref="Address" />.</param>
        /// <param name="state"> The state or province of this <see cref="Address" />.</param>
        /// <param name="zip">   The ZIP or other postal code of this <see cref="Address" />.</param>
        /// <param name="unit">  The unit number of this <see cref="Address" />, if one is required. For example, an Apartment or Room number.</param>
        public Address(string street, string city, string state, string zip, string unit = null) : this("United States", street, city, state, zip, unit) { }

        /// <summary>
        /// Creates a new <see cref="Address" /> in the specified country.
        /// </summary>
        /// <param name="country">The country this <see cref="Address" /> resides in.</param>
        /// <param name="street"> The street address for this <see cref="Address" />.</param>
        /// <param name="city">   The city of this <see cref="Address" />.</param>
        /// <param name="state">  The state or province of this <see cref="Address" />.</param>
        /// <param name="zip">    The ZIP or other postal code of this <see cref="Address" />.</param>
        /// <param name="unit">   The unit number of this <see cref="Address" />, if one is required. For example, an Apartment or Room number.</param>
        public Address(string country, string street, string city, string state, string zip, string unit = null)
        {
            VerifyZipCodeFormat(zip);

            Country = country;
            StreetAddress = street;
            City = city;
            State = state;
            PostalCode = zip;
            UnitNumber = unit;
        }

        private static void VerifyZipCodeFormat(string zip)
        {
            if (zip.Length > 5)
                if (zip[5] != '-')
                    throw new ArgumentOutOfRangeException(nameof(zip), zip[5], "Postal Code must be in the format of \"XXXXX\" or \"XXXXX-XXXX\"");
                else if (zip.Length != 10)
                    throw new ArgumentOutOfRangeException(nameof(zip), zip.Length, "Postal Code must be either 5 or 10 characters, in either the format of \"XXXXX\" or \"XXXXX-XXXX\"");
        }

        #region Property manipulation methods

        /// <summary>
        /// Creates a new <see cref="Address" /> using the provided city and the current values of this address.
        /// </summary>
        /// <param name="city">The new value for the <see cref="Address" />'s City.</param>
        /// <returns></returns>
        public Address WithCity(string city)
        {
            return new Address(Country, StreetAddress, city, State, PostalCode, UnitNumber);
        }

        /// <summary>
        /// Creates a new <see cref="Address" /> using the provided country and the current values of this address.
        /// </summary>
        /// <param name="country">The new value for the <see cref="Address" />'s Country.</param>
        /// <returns></returns>
        public Address WithCountry(string country)
        {
            return new Address(country, StreetAddress, City, State, PostalCode, UnitNumber);
        }

        /// <summary>
        /// Creates a new <see cref="Address" /> using the provided state or province and the current values of this address.
        /// </summary>
        /// <param name="state">The new value for the <see cref="Address" />'s State.</param>
        /// <returns></returns>
        public Address WithStateOrProvince(string state)
        {
            return new Address(Country, StreetAddress, City, state, PostalCode, UnitNumber);
        }

        /// <summary>
        /// Creates a new <see cref="Address" /> using the provided street address and the current values of this address.
        /// </summary>
        /// <param name="street">The new value for the <see cref="Address" />'s StreetAddress.</param>
        /// <returns></returns>
        public Address WithStreetAddress(string street)
        {
            return new Address(Country, street, City, State, PostalCode, UnitNumber);
        }

        /// <summary>
        /// Creates a new <see cref="Address" /> using the provided unit number and the current values of this address.
        /// </summary>
        /// <param name="unit">The new value for the <see cref="Address" />'s UnitNumber.</param>
        /// <returns></returns>
        public Address WithUnitNumber(string unit)
        {
            return new Address(Country, StreetAddress, City, State, PostalCode, unit);
        }

        /// <summary>
        /// Creates a new <see cref="Address" /> using the ZIP or other Postal Code and the current values of this address. Formatting will be enforced.
        /// </summary>
        /// <param name="zip">The new value for the <see cref="Address" />'s PostalCode.</param>
        /// <returns></returns>
        public Address WithZipOrPostalCode(string zip)
        {
            VerifyZipCodeFormat(zip);

            return new Address(Country, StreetAddress, City, State, zip, UnitNumber);
        }

        #endregion Property manipulation methods
    }
}