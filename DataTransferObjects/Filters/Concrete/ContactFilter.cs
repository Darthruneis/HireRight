using DataTransferObjects.Filters.Abstract;
using System;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class ContactFilter : Filter<Contact>
    {
        [DataMember]
        public string CellNumber { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool? IsAdmin { get; set; }

        [DataMember]
        public bool? IsPrimary { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string OfficeNumber { get; set; }

        public ContactFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public ContactFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");

            return query.ToString();
        }
    }
}