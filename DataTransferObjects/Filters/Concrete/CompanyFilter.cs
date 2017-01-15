using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class CompanyFilter : Filter<Company>
    {
        [DataMember]
        public AddressFilter BillingAddressFilter { get; set; }

        [DataMember]
        public ContactFilter ContactFilter { get; set; }

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

            if (BillingAddressFilter != null)
                query.Append(BillingAddressFilter.CreateQuery(nameof(BillingAddressFilter)));

            if (ContactFilter != null)
                query.Append(ContactFilter.CreateQuery(false));

            if (LocationFilter != null)
                query.Append(LocationFilter.CreateQuery(false));

            if (!string.IsNullOrWhiteSpace(Name))
                query.Append($"&filter.{nameof(Name)}={Name}");

            return query.ToString();
        }
    }
}