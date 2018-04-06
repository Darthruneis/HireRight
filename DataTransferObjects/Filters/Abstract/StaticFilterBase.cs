using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;

namespace DataTransferObjects.Filters.Abstract
{
    [Serializable]
    public abstract class StaticFilterBase : FilterBase
    {
        [DataMember]
        public List<long> ItemIds { get; set; }

        protected StaticFilterBase() : base(0, 10) { }

        protected StaticFilterBase(int page, int size, params long[] itemIds) : base(page, size)
        {
            ItemIds = itemIds.ToList();
        }
    }
}