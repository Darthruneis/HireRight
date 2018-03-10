using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HireRight.EntityFramework.CodeFirst.Abstract
{
    public class StaticPocoBase : PocoBase
    {
        [Index(IsUnique = true)]
        public long StaticId { get; set; }
    }
}