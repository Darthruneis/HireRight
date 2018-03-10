using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public class StaticPocoBase : PocoBase
    {
        /// <summary>
        /// The unique Id of the entity.
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public override long Id { get; set; }
    }
}