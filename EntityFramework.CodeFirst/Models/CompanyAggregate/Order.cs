using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
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
    /// A request to purchase some number of products.
    /// </summary>
    public class Order : PocoBase
    {
        /// <summary>
        /// Navigation property.
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Foreign key for the Company that is making this order.
        /// </summary>
        [Required]
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }

        /// <summary>
        /// The date that the order was completed.
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Notes left by the buyer about this order.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// Set of positions the buyer is interested in buying products for.
        /// </summary>
        [Required]
        public List<string> PositionsOfInterest { get; set; }

        /// <summary>
        /// The product that is being purchased in this order.
        /// </summary>
        public virtual Product Product { get; set; }

        /// <summary>
        /// Foreign key for the product that is being purchased.
        /// </summary>
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        /// <summary>
        /// The number of units being purchased of the specified product.
        /// </summary>
        [Required]
        public int Quantity { get; set; }

        /// <summary>
        /// The current state of this order.
        /// </summary>
        [Required]
        public OrderStatus Status { get; set; }

        public Order()
        {
            Status = OrderStatus.Pending;
        }
    }
}