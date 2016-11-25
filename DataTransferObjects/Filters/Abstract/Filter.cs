using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace DataTransferObjects.Filters.Abstract
{
    [Serializable]
    public abstract class Filter<TModel> : FilterBase
    {
        [DataMember]
        public List<Guid> ItemGuids { get; set; }

        protected Filter(int page, int size, params Guid[] itemGuids) : base(page, size)
        {
            ItemGuids = new List<Guid>();

            foreach (Guid itemGuid in itemGuids)
                ItemGuids.Add(itemGuid);
        }

        public virtual string CreateQuery(bool addBaseQuery = true)
        {
            StringBuilder query = new StringBuilder(addBaseQuery ? base.CreateQuery() : "");

            if (ItemGuids == null || !ItemGuids.Any())
                return query.ToString();

            query.Append($"&filter.itemGuids={string.Join(",", ItemGuids)}");

            return query.ToString();
        }
    }
}