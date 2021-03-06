﻿using HireRight.EntityFramework.EFCF.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.EFCF.Models
{
    /// <summary>
    /// A discount is a reduction in total cost or individual price of a threshold of sales. Generally thought of as a bulk discount for large orders of Tests.
    /// </summary>
    public class Discount : PocoBase
    {
        /// <summary>
        /// The discount amount to apply. A percentage discount will reduce the entire cost of the order by the specified amount (this amount should then
        /// generally be well below 1.00). Flat reduction amounts should equal the flat reduction that is to be applied to the order. These amounts should always
        /// be positive!
        /// </summary>
        [Required]
        public decimal Amount { get; }

        [NotMapped]
        public KeyValuePair<uint, decimal> ConvertToKeyValuePair => new KeyValuePair<uint, decimal>(Threshold, Amount);

        /// <summary>
        /// Whether the discount is a Percentage Discount, or the discount is a flat reduction amount.
        /// </summary>
        [Required]
        public bool IsPercent { get; private set; }

        /// <summary>
        /// The Product that is being purchased.
        /// </summary>
        [Required]
        public virtual Product Product { get; set; }

        /// <summary>
        /// Foreign key for the Product.
        /// </summary>
        [Required]
        [ForeignKey("Product")]
        public int ProductId { get; set; }

        /// <summary>
        /// The threshold is the number of units required to be purchased in order to be eligible for this discount to be applied to your order.
        /// </summary>
        [Required]
        public uint Threshold { get; }

        public Discount(bool isPercent, decimal amount, uint threshold)
        {
            IsPercent = isPercent;
            Amount = amount;
            Threshold = threshold;
        }
    }
}