using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// A Scale Category is a type of expertise or personal trait that impacts job performance and eligibility.
    /// </summary>
    public class ScaleCategory : StaticPocoBase
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
        
        public List<IndustryScaleCategory> IndustryBinders { get; set; }

        public ScaleCategory()
        {
            IndustryBinders = new List<IndustryScaleCategory>();
        }

        public ScaleCategory(string title, string description) : this()
        {
            Title = title;
            Description = description;
        }

        /// <summary>Returns a string that represents the current object.</summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"{StaticId}: {Title}";
        }
    }
}