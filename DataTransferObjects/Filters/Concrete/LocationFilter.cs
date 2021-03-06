﻿using DataTransferObjects.Filters.Abstract;
using System;
using System.Runtime.Serialization;
using System.Text;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace DataTransferObjects.Filters.Concrete
{
    [Serializable]
    public class LocationFilter : Filter<CompanyLocation>
    {
        [DataMember]
        public AddressFilter AddressFilter { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Label { get; set; }

        public LocationFilter(int page, int size, params Guid[] itemGuids) : base(page, size, itemGuids)
        { }

        public LocationFilter() : base(1, 10)
        {
        }

        public override string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery(true) : "");

            if (AddressFilter != null)
                query.Append(AddressFilter.CreateQuery(nameof(AddressFilter)));

            return query.ToString();
        }
    }
}