using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

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
    public class OrderFilter : Filter<Order>
    {
        [DataMember]
        public DateTime? Completed { get; set; }

        [DataMember]
        public DateTimeSearchComparators? CompletedComparator { get; set; }

        [DataMember]
        public string Notes { get; set; }

        [DataMember]
        public IEnumerable<OrderStatus> OrderStatus { get; set; }

        [DataMember]
        public List<string> PositionsOfInterest { get; set; }

        [DataMember]
        public ProductFilter ProductFilter { get; set; }

        [DataMember]
        public int? Quantity { get; set; }

        [DataMember]
        public NumericSearchComparators? QuantityComparator { get; set; }

        public OrderFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public OrderFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");
            return query.ToString();
        }
    }
}