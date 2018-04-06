﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Models.CompanyAggregate
{
    /// <summary>
    /// An industry to which certain <see cref="ScaleCategory"/>s may be relevant.
    /// </summary>
    public class Industry : StaticPocoBase
    {
        public const long HealthCare = 1;
        public const long Management = 2;
        public const long CustomerService = 3;
        public const long Sales = 4;
        public const long Manufacturing = 5;
        public const long Office = 6;
        public const long General = 7;
        public const long Pharmaceutical = 8;
        public const long Other = 9;

        /// <summary>
        /// The name of the industry.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; private set; }
        
        public virtual List<IndustryScaleCategory> CategoryBinders { get; set; }

        public ICollection<IndustryAssessmentBinder> Assessments { get; private set; }

        private Industry()
        {
            CategoryBinders = new List<IndustryScaleCategory>();
            Assessments = new List<IndustryAssessmentBinder>();
        }

        public Industry(long id, string name) : this()
        {
            StaticId = id;
            Name = name;
        }
    }
}