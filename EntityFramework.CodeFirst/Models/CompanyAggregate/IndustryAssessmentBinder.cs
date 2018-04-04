using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Models.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    public class IndustryAssessmentBinder : PocoBase
    {
        public long AssessmentId { get; private set; }
        [ForeignKey(nameof(AssessmentId))]
        public Assessment Assessment { get; private set; }

        public long IndustryId { get; private set; }
        [ForeignKey(nameof(IndustryId))]
        public Industry Industry { get; private set; }

        /// <summary>
        /// Default constructor. Required for EF.
        /// </summary>
        private IndustryAssessmentBinder() { }

        public IndustryAssessmentBinder(long assessmentId, long industryId)
        {
            AssessmentId = assessmentId;
            IndustryId = industryId;
        }
    }
}