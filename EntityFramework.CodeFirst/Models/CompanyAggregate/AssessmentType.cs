using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
{
    public class AssessmentType : StaticPocoBase
    {
        public const long Test = 1;
        public const long Profile = 2;

        [Index(IsUnique = true)]
        [StringLength(120)]
        [Required]
        public string Name { get; private set; }

        /// <summary>
        /// Default constructor. Required for EF.
        /// </summary>
        private AssessmentType() { }

        public AssessmentType(string name, long id)
        {
            Name = name;
            StaticId = id;
        }
    }
}