using HireRight.EntityFramework.CodeFirst.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    /// <summary>
    /// An account is a relationship between a Company and its Clients and Orders.
    /// </summary>
    public class Account : PocoBase
    {
        public virtual ICollection<Client> Clients { get; set; }

        public virtual Company Company { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }

        [Required]
        public string Notes { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}