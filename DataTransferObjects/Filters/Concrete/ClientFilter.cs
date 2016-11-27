using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class ClientFilter : Filter<Client>
    {
        [DataMember]
        public AccountFilter AccountFilter { get; set; }

        [DataMember]
        public ContactFilter AdminFilter { get; set; }

        [DataMember]
        public ContactFilter PrimaryContactFilter { get; set; }

        public ClientFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public ClientFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");
            return query.ToString();
        }
    }
}