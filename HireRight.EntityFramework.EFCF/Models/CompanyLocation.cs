﻿using HireRight.EntityFramework.EFCF.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.EFCF.Models
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

        public virtual Company Company { get; set; }

        [ForeignKey("Company")]
        public int CompanyId { get; set; }

        /// <summary>
        /// A description for the location. Entirely up to the company as to what is provided here, but examples could be a description which indicates whether
        /// the location is a call center, retail store, or office building.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// A company-determined label for the location. e.g. "Brunswick", "Center Road" or "Store #66"
        /// </summary>
        public string Label { get; set; }
    }
}