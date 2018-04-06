using DataTransferObjects.Filters.Abstract;
using System;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    public class AddressFilter : Filter
    {
        public string City { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string State { get; set; }
        public string StreetAddress { get; set; }
        public string UnitNumber { get; set; }

        public AddressFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        {
        }

        public string CreateQuery(string propertyName)
        {
            string prefix = $"&filter.{propertyName}.";
            StringBuilder query = new StringBuilder("");

            if (!string.IsNullOrWhiteSpace(City))
                query.Append(prefix + $"{nameof(City)}={City}");

            if (!string.IsNullOrWhiteSpace(Country))
                query.Append(prefix + $"{nameof(Country)}={Country}");

            if (!string.IsNullOrWhiteSpace(PostalCode))
                query.Append(prefix + $"{nameof(PostalCode)}={PostalCode}");

            if (!string.IsNullOrWhiteSpace(State))
                query.Append(prefix + $"{nameof(State)}={State}");

            if (!string.IsNullOrWhiteSpace(StreetAddress))
                query.Append(prefix + $"{nameof(StreetAddress)}={StreetAddress}");

            if (!string.IsNullOrWhiteSpace(UnitNumber))
                query.Append(prefix + $"{nameof(UnitNumber)}={UnitNumber}");

            return query.Append("}").ToString();
        }
    }
}