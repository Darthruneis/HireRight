using System.ComponentModel.DataAnnotations.Schema;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
{
    public class IndustryScaleCategory : PocoBase
    {
        [ForeignKey(nameof(Industry))]
        public long IndustryId { get; set; }

        [ForeignKey(nameof(Category))]
        public long CategoryId { get; set; }

        public Industry Industry { get; private set; }

        public ScaleCategory Category { get; private set; }

        private IndustryScaleCategory() { }

        public IndustryScaleCategory(long industryId, long categoryId)
        {
            IndustryId = industryId;
            CategoryId = categoryId;
        }
    }
}