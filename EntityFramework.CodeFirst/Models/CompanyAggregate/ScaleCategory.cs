using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
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

        [Index(IsUnique = true)]
        public long StaticId { get; set; }
        
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
    }
}