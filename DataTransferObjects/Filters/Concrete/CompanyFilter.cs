using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class CompanyFilter : Filter<Company>
    {
        [DataMember]
        public string BillingAddress { get; set; }

        [DataMember]
        public ClientFilter ClientFilter { get; set; }

        [DataMember]
        public LocationFilter LocationFilter { get; set; }

        [DataMember]
        public string Name { get; set; }

        public CompanyFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public CompanyFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery(true) : "");

            if (!string.IsNullOrWhiteSpace(BillingAddress))
                query.Append($"&filter.{nameof(BillingAddress)}={BillingAddress}");

            if (ClientFilter != null)
                query.Append(ClientFilter.CreateQuery(false));

            if (LocationFilter != null)
                query.Append(LocationFilter.CreateQuery(false));

            if (!string.IsNullOrWhiteSpace(Name))
                query.Append($"&filter.{nameof(Name)}={Name}");

            return query.ToString();
        }
    }
}