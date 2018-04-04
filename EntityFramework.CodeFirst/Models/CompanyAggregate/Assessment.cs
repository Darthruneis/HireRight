using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Models.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    public class Assessment : PocoBase
    {
        [Index(IsUnique = true)]
        [StringLength(400)]
        [Required]
        public string Title { get; private set; }

        public int? Duration { get; private set; }

        public bool IsTimed { get; private set; }

        public long AssessmentTypeId { get; private set; }

        [ForeignKey(nameof(AssessmentTypeId))]
        public AssessmentType AssessmentType { get; private set; }

        public ICollection<IndustryAssessmentBinder> Industries { get; private set; }

        public ICollection<AssessmentScaleCategoryBinder> Categories { get; private set; }

        /// <summary>
        /// Default constructor. Required for EF.
        /// </summary>
        private Assessment()
        {
            Industries = new List<IndustryAssessmentBinder>();
            Categories = new List<AssessmentScaleCategoryBinder>();
        }

        public Assessment(string title, long assessmentTypeId, int? duration = null, bool isTimed = false) : this()
        {
            Title = title;
            AssessmentTypeId = assessmentTypeId;
            Title = title;
            Duration = duration;
            IsTimed = isTimed;
            AssessmentTypeId = assessmentTypeId;
        }
    }
}