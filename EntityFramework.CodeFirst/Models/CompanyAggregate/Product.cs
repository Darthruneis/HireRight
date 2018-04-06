using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
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

        internal Product()
        {
        }

        private Product(List<Discount> discounts = null) : this()
        {
            Discounts = discounts ?? new List<Discount>();
        }

        private Product(string title, decimal price, List<Discount> discounts = null) : this(discounts)
        {
            Title = title;
            Price = price;
        }

        public Product(string title, decimal price, long id, List<Discount> discounts = null) : this(title, price, discounts)
        {
            StaticId = id;
        }

        public Product(string title, decimal price, Guid rowGuid, List<Discount> discounts = null) : this(title, price, discounts)
        {
            RowGuid = rowGuid;
        }

        public Discount GetHighestDiscountForQuantity(long quantity)
        {
            return Discounts?
                   .Where(x => x.IsActive)
                   .OrderByDescending(x => x.Threshold)
                   .FirstOrDefault(x => x.Threshold <= quantity);
        }
    }
}