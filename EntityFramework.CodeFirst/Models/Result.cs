using System.Collections.Generic;

namespace HireRight.EntityFramework.CodeFirst.Models
{
    public class PageResult<T> : Result
        where T : class
    {
        public long PageNumber { get; private set; }
        public long PageSize { get; private set; }
        public ICollection<T> Results { get; private set; }
        public long TotalMatchingResults { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected PageResult(string error, bool isSuccess, long pageNumber, ICollection<T> results, long pageSize, long totalMatchingResults) : base(error, isSuccess)
        {
            PageNumber = pageNumber;
            Results = results;
            PageSize = pageSize;
            TotalMatchingResults = totalMatchingResults;
        }

        public static PageResult<T> Fail(string errorMessage, long pageNumber = 1, long pageSize = 10)
            => new PageResult<T>(errorMessage, false, pageNumber, new List<T>(), pageSize, 0);

        public static PageResult<T> Ok(long totalMatchingResults, ICollection<T> results, long pageNumber = 1, long pageSize = 10)
                    => new PageResult<T>(null, true, pageNumber, results, pageSize, totalMatchingResults);
    }

    public class Result
    {
        public string Error { get; private set; }
        public bool IsFailure => !IsSuccess;
        public bool IsSuccess { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected Result(string error, bool isSuccess)
        {
            Error = error;
            IsSuccess = isSuccess;
        }

        public static Result Fail(string errorMessage) => new Result(errorMessage, false);

        public static Result<T> Fail<T>(string error)
            where T : class
            => Result<T>.Fail(error);

        public static PageResult<T> Fail<T>(string errorMessage, long pageNumber, long pageSize)
            where T : class
            => PageResult<T>.Fail(errorMessage, pageNumber, pageSize);

        public static Result Ok() => new Result(null, true);

        public static Result<T> Ok<T>(T results)
            where T : class
            => Result<T>.Ok(results);

        public static PageResult<T> Ok<T>(long totalMatchingResults, ICollection<T> results, long pageNumber, long pageSize)
            where T : class
            => PageResult<T>.Ok(totalMatchingResults, results, pageNumber, pageSize);
    }

    public class Result<T> : Result
        where T : class
    {
        public T Results { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:System.Object" /> class.
        /// </summary>
        protected Result(string error, bool isSuccess, T results) : base(error, isSuccess)
        {
            Results = results;
        }

        public new static Result<T> Fail(string errorMessage) => new Result<T>(errorMessage, false, null);

        public static Result<T> Ok(T results) => new Result<T>(null, true, results);
    }
}