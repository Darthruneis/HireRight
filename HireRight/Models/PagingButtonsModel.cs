using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataTransferObjects.Filters.Abstract;

namespace HireRight.Models
{
    public class PagingButtonsModel
    {
        public FilterBase Filter { get; set; }
        public long TotalMatchingResults { get; set; }

        public PagingButtonsModel(FilterBase filter, long totalMatchingResults)
        {
            Filter = filter;
            TotalMatchingResults = totalMatchingResults;
        }
    }
}