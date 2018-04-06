using DataTransferObjects.Filters.Abstract;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class ContactFilter : Filter
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

        protected override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");

            return query.ToString();
        }
    }
}