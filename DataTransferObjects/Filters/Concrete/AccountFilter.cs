using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class AccountFilter : Filter<Account>
    {
        [DataMember]
        public string Notes { get; set; }

        public AccountFilter(int page, int size, string notesText, params Guid[] itemGuids) : base(page, size, itemGuids)
        {
            Notes = notesText;
        }
    }
}