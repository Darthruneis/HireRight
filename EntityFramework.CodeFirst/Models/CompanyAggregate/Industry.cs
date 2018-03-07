using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HireRight.EntityFramework.CodeFirst.Abstract;

namespace HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate
{
    /// <summary>
    /// An industry to which certain <see cref="ScaleCategory"/>s may be relevant.
    /// </summary>
    public class Industry
    {
        public const long Medical = 1;
        public const long Management = 2;
        public const long CustomerServiceSales = 3;
        public const long Startup = 4;
        public const long Manufacturing = 5;
        public const long AdministrationClerical = 6;
        public const long General = 7;
        public const long ProfessionalServices = 8;
        public const long Other = 9;

        /// <summary>
        /// The time that the entity was created in the database.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedUtc { get; set; }

        /// <summary>
        /// The unique Id of the entity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// The time stamp of the entity's state changes.
        /// </summary>
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public Guid RowGuid { get; set; }

        /// <summary>
        /// The name of the industry.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Index(IsUnique = true)]
        public string Name { get; private set; }
        
        public virtual List<IndustryScaleCategory> CategoryBinders { get; set; }

        private Industry()
        {
            CategoryBinders = new List<IndustryScaleCategory>();
        }

        public Industry(long id, string name) : this()
        {
            Id = id;
            Name = name;
        }
    }
}