using System.Collections.Generic;
using System.Data.Entity.Validation;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Concrete
{
    /// <summary>
    /// Idea I had at one point that I don't think I'll be using any time soon, but not quite ready to delete this yet.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RepositoryResponseObject<T> where T : PocoBase
    {
        public List<DbValidationError> Errors { get; set; }

        public List<T> Items { get; set; }

        public int TotalResults { get; set; }

        public RepositoryResponseObject(List<T> itemList, int totalResults, List<DbValidationError> errors = null) : this()
        {
            Items = itemList;
            TotalResults = totalResults;
            if (errors != null) Errors = errors;
        }

        /// <summary>
        /// Less code duplication.
        /// </summary>
        private RepositoryResponseObject()
        {
            Items = new List<T>();
            TotalResults = 0;
            Errors = new List<DbValidationError>();
        }
    }
}