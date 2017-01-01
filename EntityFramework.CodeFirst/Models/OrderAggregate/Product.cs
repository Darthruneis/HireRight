using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract.ContainingCollections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HireRight.EntityFramework.CodeFirst.Models.OrderAggregate
{
    /// <summary>
    /// A product is an item which can be purchased through HireRight.
    /// </summary>
    public class Product : PocoBase, IContainingDiscounts
    {
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

        public Product(string title, decimal price, List<Discount> discounts = null) : this(discounts)
        {
            Title = title;
            Price = price;
        }

        public Product(List<Discount> discounts = null)
        {
            Discounts = discounts ?? new List<Discount>();
        }

        public Product()
        {
        }
    }
}