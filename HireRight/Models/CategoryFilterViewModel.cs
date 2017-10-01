using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.Models
{
    public class CategoryFilterViewModel
    {
        public CategoryFilter Filter { get; set; }
        public long TotalMatchingResults { get; set; }
    }
}