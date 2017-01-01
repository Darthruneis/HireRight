using HireRight.EntityFramework.CodeFirst.Abstract;
using System.ComponentModel.DataAnnotations;

namespace HireRight.EntityFramework.CodeFirst.Models.OrderAggregate
{
    /// <summary>
    /// A Scale Category is a type of expertise or personal trait that impacts job performance and eligibility.
    /// </summary>
    public class ScaleCategory : PocoBase
    {
        /// <summary>
        /// The description for this scale category.
        /// </summary>
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// The Title of this scale category.
        /// </summary>
        [Required]
        public string Title { get; set; }

        public ScaleCategory()
        {
        }

        public ScaleCategory(string title, string description)
        {
            Title = title;
            Description = description;
        }
    }
}