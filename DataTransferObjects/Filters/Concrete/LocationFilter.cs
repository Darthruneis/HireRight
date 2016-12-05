using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class LocationFilter : Filter<CompanyLocation>
    {
        [DataMember]
        public Address Address { get; set; }

        [DataMember]
        public CompanyFilter CompanyFilter { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Label { get; set; }

        public LocationFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public LocationFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");

            if (Address != null)
                query.Append(Address.CreateQuery(nameof(Address)));

            return query.ToString();
        }
    }
}