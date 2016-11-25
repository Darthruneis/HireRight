using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class ContactFilter : Filter<Contact>
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string CellNumber { get; set; }

        [DataMember]
        public ClientFilter ClientFilter { get; set; }

        [DataMember]
        public CompanyFilter CompanyFilter { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public bool IsAdmin { get; set; }

        [DataMember]
        public bool IsPrimary { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string OfficeNumber { get; set; }

        public ContactFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }
    }
}