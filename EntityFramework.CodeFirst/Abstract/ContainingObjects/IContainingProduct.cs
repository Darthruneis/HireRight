using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public interface IContainingProduct
    {
        /// <summary>
        /// The Product that is being purchased.
        /// </summary>
        Product Product { get; set; }

        /// <summary>
        /// Foreign key for the Product.
        /// </summary>
        Guid ProductId { get; set; }
    }
}