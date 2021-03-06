﻿using System;
using System.ComponentModel.DataAnnotations;
using HireRight.EntityFramework.CodeFirst.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// A discount is a reduction in total cost or individual price of a threshold of sales. Generally thought of as a bulk discount for large orders.
    /// </summary>
    public class Discount : PocoBase
    {
        /// <summary>
        /// The discount amount to apply. A percentage discount will reduce the entire cost of the order by the specified amount (this amount should then
        /// generally be well below 1.00). Flat reduction amounts should equal the flat reduction that is to be applied to the order. These amounts should always
        /// be positive!
        /// </summary>
        [Required]
        public decimal Amount { get; private set; }

        /// <summary>
        /// Whether the discount is a Percentage Discount, or the discount is a flat reduction amount.
        /// </summary>
        [Required]
        public bool IsPercent { get; private set; }

        /// <summary>
        /// Foreign key for the Product this Discount applies to.
        /// </summary>
        public Guid ProductId { get; set; }

        /// <summary>
        /// The threshold is the number of units required to be purchased in order to be eligible for this discount to be applied to your order.
        /// </summary>
        [Required]
        public int Threshold { get; private set; }

        public Discount()
        {
        }

        public Discount(bool isPercent, decimal amount, int threshold)
        {
            IsPercent = isPercent;
            Amount = amount;
            Threshold = threshold;
        }
    }
}