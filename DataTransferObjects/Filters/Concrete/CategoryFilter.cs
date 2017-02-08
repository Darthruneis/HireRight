using System;
using System.ComponentModel.DataAnnotations;
using System.Text;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Abstract;

namespace DataTransferObjects.Filters.Concrete
{
    public class CategoryFilter : Filter<CategoryDTO>
    {
        [Display(Name = "Description")]
        public string DescriptionFilter { get; set; }

        [Display(Name = "Title")]
        public string TitleFilter { get; set; }

        public CategoryFilter() : base(1, 10)
        {
        }

        public CategoryFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery(true) : "");

            if (!string.IsNullOrWhiteSpace(DescriptionFilter))
                query.Append($"&filter.descriptionFilter={DescriptionFilter}");
            if (!string.IsNullOrWhiteSpace(TitleFilter))
                query.Append($"&filter.titleFilter={TitleFilter}");

            return query.ToString();
        }
    }
}