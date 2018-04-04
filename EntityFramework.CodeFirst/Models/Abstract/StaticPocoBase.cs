using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Models.Abstract
{
    public class StaticPocoBase : PocoBase
    {
        [Index(IsUnique = true)]
        public long StaticId { get; set; }
    }
}