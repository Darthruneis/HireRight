using System;
using System.Collections.Generic;
using System.Linq;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.BusinessLogic.Extensions
{
    public static class ResultExtensions
    {
        public static PagingResultDTO<TDto> PageResultToDto<TDto, TModel>(this PageResult<TModel> pageResult, Func<TModel, TDto> conversionMethod)
            where TDto : class
            where TModel : PocoBase
        {
            return new PagingResultDTO<TDto>()
            {
                IsSuccess = pageResult.IsSuccess,
                PageNumber = pageResult.PageNumber,
                PageSize = pageResult.PageSize,
                TotalMatchingResults = pageResult.TotalMatchingResults,
                PageResult = pageResult.IsSuccess ? pageResult.Results.Select(conversionMethod).ToList() : new List<TDto>()
            };
        }
    }
}