using DataTransferObjects.Filters.Abstract;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    public enum DateTimeSearchComparators
    {
        [DataMember]
        Before,

        [DataMember]
        DayOf,

        [DataMember]
        HourOf,

        [DataMember]
        Exactly,

        [DataMember]
        After
    }

    [Serializable]
    public class OrderFilter : Filter
    {
        [DataMember]
        public DateTime? Completed { get; set; }

        [DataMember]
        public DateTimeSearchComparators? CompletedComparator { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public IEnumerable<long> OrderStatus { get; set; }

        [DataMember]
        public List<string> PositionsOfInterest { get; set; }

        [DataMember]
        public int? Quantity { get; set; }

        [DataMember]
        public NumericSearchComparators? QuantityComparator { get; set; }

        public OrderFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public OrderFilter() : base(1, 10)
        {
        }

        protected override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");
            return query.ToString();
        }
    }
}