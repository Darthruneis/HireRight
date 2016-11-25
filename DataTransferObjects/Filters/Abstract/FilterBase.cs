using System;
using System.Runtime.Serialization;

namespace DataTransferObjects.Filters.Abstract
{
    [Serializable]
    public abstract class FilterBase
    {
        [DataMember]
        public int PageNumber { get; set; }

        [DataMember]
        public int PageSize { get; set; }

        protected FilterBase(int page, int size)
        {
            //Pages should start at 1, not zero, because 'page 0' doesn't make as much sense as 'page 1' for starting a paging system
            //to anyone other than a Developer
            if (page < 1) page = 1;

            //Do not allow a page size less than 1 or above 100
            if (size < 1) size = 1;
            else if (size > 100) size = 100;

            PageNumber = page;
            PageSize = size;
        }

        protected string CreateQuery()
        {
            return $"?filter.pageNumber={PageNumber}&filter.pageSize={PageSize}";
        }
    }
}