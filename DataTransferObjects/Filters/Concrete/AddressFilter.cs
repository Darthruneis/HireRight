using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Abstract;
using System;

namespace DataTransferObjects.Filters.Concrete
{
    public class AddressFilter : Filter<AddressDTO>
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
    }
}