using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HireRight.EntityFramework.CodeFirst.Models.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// A product is an item which can be purchased through HireRight.
    /// </summary>
    public class Product : StaticPocoBase
    {
        public const long AssessmentTest = 1;
        /// <summary>
        /// A collection of discounts which can be applied to this product based on volume.
        /// </summary>
        public virtual ICollection<Discount> Discounts { get; set; }

        /// <summary>
        /// The price of the product.
        /// </summary>
        [Range(0.05, 100000)]
        [Required]
        public decimal Price { get; set; }

        /// <summary>
        /// The Title of the product.
        /// </summary>
        [Required]
        public string Title { get; set; }

        public Product(string title, decimal price, long id, List<Discount> discounts = null) : this(title, price, discounts)
        {
            StaticId = id;
        }

        public Product(string title, decimal price, Guid rowGuid, List<Discount> discounts = null) : this(title, price, discounts)
        {
            RowGuid = rowGuid;
        }

        public Product(List<Discount> discounts = null)
        {
            Discounts = discounts ?? new List<Discount>();
        }

        private Product(string title, decimal price, List<Discount> discounts = null) : this(discounts)
        {
            Title = title;
            Price = price;
        }

        private Product()
        {
        }
    }
}