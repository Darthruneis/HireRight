using HireRight.EntityFramework.CodeFirst.Abstract;
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
        public Address Address { get; set; }

        [Required]
        public string CellNumber { get; set; }

        public virtual Client Client { get; set; }

        [ForeignKey("Client")]
        public Guid ClientId { get; set; }

        public virtual Company Company { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public bool IsAdmin { get; set; }

        [Required]
        public bool IsPrimary { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string OfficeNumber { get; set; }
    }
}