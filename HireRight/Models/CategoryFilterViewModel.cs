using DataTransferObjects.Filters.Concrete;

namespace HireRight.Models
{
    public class CategoryFilterViewModel
    {
        public CategoryFilter Filter { get; set; }
        public long TotalMatchingResults { get; set; }
    }
}