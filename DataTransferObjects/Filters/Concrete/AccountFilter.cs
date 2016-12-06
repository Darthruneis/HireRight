using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class AccountFilter : Filter<Account>
    {
        [DataMember]
        public CompanyFilter CompanyFilter { get; set; }

        [DataMember]
        public string Notes { get; set; }

        public AccountFilter(int page, int size, string notesText, params Guid[] itemGuids) : base(page, size, itemGuids)
        {
            Notes = notesText;
        }

        public AccountFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");
            return query.ToString();
        }
    }
}