using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class LocationFilter : Filter<CompanyLocation>
    {
        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public CompanyFilter CompanyFilter { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Label { get; set; }

        public LocationFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }
    }
}