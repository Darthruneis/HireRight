using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// A Contact is an individual that is part of a Company and/or Client, and can optionally be a Primary contact or an Administrator.
    /// </summary>
    public class Contact : PocoBase
    {
        /// <summary>
        /// The address of this contact.
        /// </summary>
        [Required]
        public Address Address { get; set; }

        /// <summary>
        /// This contact's cell phone number.
        /// </summary>
        [Required]
        public string CellNumber { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Foreign key for this Contact's Company.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// This contact's email address.
        /// </summary>
        [Required]
        public string Email { get; set; }

        /// <summary>
        /// Whether or not this contact is the Administrator.
        /// </summary>
        [Required]
        public bool IsAdmin { get; set; }

        /// <summary>
        /// Whether or not this contact is the Primary Contact.
        /// </summary>
        [Required]
        public bool IsPrimary { get; set; }

        /// <summary>
        /// This contact's name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// This contact's office or work number.
        /// </summary>
        [Required]
        public string OfficeNumber { get; set; }
    }
}