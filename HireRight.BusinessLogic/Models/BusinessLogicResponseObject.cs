using System.Collections.Generic;
using System.Data.Entity.Validation;
using HireRight.EntityFramework.CodeFirst.Models.Abstract;

namespace HireRight.BusinessLogic.Models
{
    public class BusinessLogicResponseObject<T> where T : PocoBase
    {
        public List<DbValidationError> Errors { get; set; }

        public List<T> Items { get; set; }

        public int TotalResults { get; set; }

        public BusinessLogicResponseObject(List<T> itemList, int totalResults, List<DbValidationError> errors = null)
            : this()
        {
            Items = itemList;
            TotalResults = totalResults;
            if (errors != null) Errors = errors;
        }

        private BusinessLogicResponseObject()
        {
            Items = new List<T>();
            TotalResults = 0;
            Errors = new List<DbValidationError>();
        }
    }
}