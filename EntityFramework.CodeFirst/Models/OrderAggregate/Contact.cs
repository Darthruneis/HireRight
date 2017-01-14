﻿using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingObjects;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models.OrderAggregate
{
    /// <summary>
    /// A Contact is an individual that is part of a Company and/or Client, and can optionally be a Primary contact or an Administrator.
    /// </summary>
    public class Contact : PocoBase, IContainingClient, IContainingCompany, IContainingAddress
    {
        /// <summary>
        /// The address of this contact.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// This contact's cell phone number.
        /// </summary>
        [Required]
        public string CellNumber { get; set; }

        /// <summary>
        /// The client this contact is related to.
        /// </summary>
        public virtual Client Client { get; set; }

        /// <summary>
        /// Foreign key for this contact's client.
        /// </summary>
        [ForeignKey("Client")]
        public Guid ClientId { get; set; }

        /// <summary>
        /// The company this contact is related to.
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Foreign key for this contact's company.
        /// </summary>
        [ForeignKey("Company")]
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