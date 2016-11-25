using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;

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
    }
}