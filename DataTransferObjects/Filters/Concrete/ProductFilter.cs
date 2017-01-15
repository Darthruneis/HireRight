using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class ProductFilter : Filter<Product>
    {
        [DataMember]
        public DiscountFilter DiscountFilter { get; set; }

        [DataMember]
        public decimal? Price { get; set; }

        [DataMember]
        public NumericSearchComparators? PriceComparator { get; set; }

        [DataMember]
        public string Title { get; set; }

        public ProductFilter() : base(1, 10)
        {
        }

        public ProductFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public new string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");

            if (DiscountFilter != null)
                query.Append(DiscountFilter.CreateQuery(false).Replace("filter.", $"filter.{nameof(DiscountFilter)}."));

            if (PriceComparator != null && Price != null)
                query.Append($"&filter.Price={PriceComparator.Value}&filter.Price={Price.Value}");

            if (!string.IsNullOrWhiteSpace(Title))
                query.Append($"&filter.Title={Title}");

            return query.ToString();
        }
    }
}