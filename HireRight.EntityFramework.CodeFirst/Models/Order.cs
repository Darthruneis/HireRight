using HireRight.EntityFramework.CodeFirst.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    /// <summary>
    /// Possible states for an Order to be in.
    /// </summary>
    public enum OrderStatus
    {
        /// <summary>
        /// The order has been submitted but not yet reviewed by a Consultant.
        /// </summary>
        Pending,

        /// <summary>
        /// The order has been reviewed by a Consultant.
        /// </summary>
        Confirmed,

        /// <summary>
        /// The order has been completed, including goods added to the customer's account, and payment having been charged/accepted.
        /// </summary>
        Completed
    }

    /// <summary>
    /// </summary>
    public class Order : PocoBase
    {
        public virtual Company Company { get; set; }

        [ForeignKey("Company")]
        public Guid CompanyId { get; set; }

        public DateTime? Completed { get; set; }

        [Required]
        public string Notes { get; set; }

        [Required]
        public List<string> PositionsOfInterest { get; set; }

        public virtual Product Product { get; set; }

        [ForeignKey("Product")]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public OrderStatus Status { get; set; }

        public Order()
        {
            Status = OrderStatus.Pending;
        }
    }
}