using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
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
        
        public List<IndustryScaleCategory> IndustryBinders { get; internal set; }
        public ICollection<AssessmentScaleCategoryBinder> Assessments { get; private set; }

        public ScaleCategory()
        {
            IndustryBinders = new List<IndustryScaleCategory>();
            Assessments = new List<AssessmentScaleCategoryBinder>();
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