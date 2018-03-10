﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    /// <summary>
    /// Defines a basic Plain Old CLR Object entity that will be stored in the database.
    /// </summary>
    public abstract class PocoBase
    {
        /// <summary>
        /// The unique Id of the entity.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public virtual long Id { get; set; }

        /// <summary>
        /// The time that the entity was created in the database.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public virtual DateTime CreatedUtc { get; set; }
        
        /// <summary>
        /// The time stamp of the entity's state changes.
        /// </summary>
        [Timestamp]
        public byte[] TimeStamp { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public virtual Guid RowGuid { get; set; }
    }
}