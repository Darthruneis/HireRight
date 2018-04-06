using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
{
    public class AssessmentScaleCategoryBinder : PocoBase
    {
        public long AssessmentId { get; private set; }
        public Assessment Assessment { get; private set; }

        public long ScaleCategoryId { get; private set; }
        public ScaleCategory ScaleCategory { get; private set; }

        /// <summary>
        /// Default constructor. Required for EF.
        /// </summary>
        private AssessmentScaleCategoryBinder() { }

        public AssessmentScaleCategoryBinder(long assessmentId, long scaleCategoryId)
        {
            AssessmentId = assessmentId;
            ScaleCategoryId = scaleCategoryId;
        }
    }
}