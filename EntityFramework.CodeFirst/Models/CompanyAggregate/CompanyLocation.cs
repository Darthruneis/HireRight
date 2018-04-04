using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Models.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// A location is a physical address where a company does business. For example, a call center, or corporate office, or retail store.
    /// </summary>
    public class CompanyLocation : PocoBase
    {
        /// <summary>
        /// The physical address of the location.
        /// </summary>
        [Required]
        public Address Address { get; set; }

        /// <summary>
        /// Navigation property.
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Foreign key for this Company Location's Company.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Company))]
        public long CompanyId { get; set; }

        /// <summary>
        /// A description for the location. Entirely up to the company as to what is provided here, but examples could be a description which indicates whether
        /// the location is a call center, retail store, or office building.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// A company-determined label for the location. e.g. "Brunswick", "Center Road" or "Store #66"
        /// </summary>
        [Required]
        public string Label { get; set; }
    }
}