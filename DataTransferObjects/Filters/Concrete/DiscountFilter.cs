using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public enum NumericSearchComparators
    {
        [DataMember]
        GreaterThan,

        [DataMember]
        GreaterThanOrEqualTo,

        [DataMember]
        EqualTo,

        [DataMember]
        LessThan,

        [DataMember]
        LessThanOrEqualTo
    }

    [Serializable]
    public class DiscountFilter : Filter<Discount>
    {
        [DataMember]
        public decimal? Amount { get; set; }

        [DataMember]
        public NumericSearchComparators? AmountComparator { get; set; }

        [DataMember]
        public bool? IsPercent { get; set; }

        [DataMember]
        public ProductFilter ProductFilter { get; set; }

        [DataMember]
        public int? Threshold { get; set; }

        [DataMember]
        public NumericSearchComparators? ThresholdComparator { get; set; }

        public DiscountFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public DiscountFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");
            return query.ToString();
        }
    }
}