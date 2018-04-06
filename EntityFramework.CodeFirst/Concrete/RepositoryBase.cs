using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Abstract;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.Abstract;

namespace HireRight.Persistence.Concrete
{
    public abstract class RepositoryWithContextFunc
    {
        protected readonly Func<HireRightDbContext> ContextFunc;

        protected RepositoryWithContextFunc(Func<HireRightDbContext> contextFunc)
        {
            ContextFunc = contextFunc;
        }

        protected RepositoryWithContextFunc() : this(() => new HireRightDbContext()) { }
    }

    public abstract class RepositoryBase<TModel> : RepositoryWithContextFunc
        where TModel : PocoBase
    {
        protected RepositoryBase(Func<HireRightDbContext> contextFunc) : base(contextFunc) { }

        protected async Task<TModel> AddBase(TModel itemToAdd, DbSet<TModel> dbSet, HireRightDbContext context)
        {
            var errors = new List<ValidationResult>();
            if (itemToAdd == null)
            {
                errors.Add(new ValidationResult(nameof(TModel) + " must not be null!"));
                throw new ApplicationException(GetConcatenatedErrorMessage(errors));
            }

            TModel result = dbSet.Add(itemToAdd);
            await context.SaveChangesAsync();

            if (result.Id == 0)
                errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " - Id was not updated."));

            if (result.RowGuid == Guid.Empty)
                errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " - RowGuid was not updated."));

            if (result.CreatedUtc == default(DateTime))
                errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " CreatedUtc was not updated."));

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        protected async Task<TModel> GetBase(Guid itemGuid, IQueryable<TModel> query, params Expression<Func<TModel, object>>[] includes)
        {
            if (itemGuid == Guid.Empty)
                throw new InvalidOperationException("Id of 0 cannot be found in the repository!");

            TModel item = await CheckForValidItem(query, itemGuid, includes).ConfigureAwait(false);
            return item;
        }

        protected async Task<TModel> GetBase(long itemGuid, IQueryable<TModel> query, params Expression<Func<TModel, object>>[] includes)
        {
            if (itemGuid == 0)
                throw new InvalidOperationException("Id of 0 cannot be found in the repository!");

            TModel item = await CheckForValidItem(query, itemGuid, includes).ConfigureAwait(false);
            return item;
        }

        protected Task<PageResult<TModel>> TakePage(IQueryable<TModel> query, FilterBase filterParameters, params Expression<Func<TModel, object>>[] includes)
            => TakePage(query, filterParameters, x => x.Id, includes);

        protected async Task<PageResult<TM>> TakePage <TM, TO>(IQueryable<TM> query, FilterBase filterParameters, 
                                                           Expression<Func<TM, TO>> orderBy = null, 
                                                           params Expression<Func<TM, object>>[] includes)
        where TM: class
        {
            query = query.Includes(includes);
            if (orderBy != null)
                query = query.OrderBy(orderBy);

            var count = query.Count();

            PageResult<TM> CreateResult(List<TM> collection) => PageResult<TM>.Ok(count, collection, filterParameters.PageNumber, filterParameters.PageSize);

            if (filterParameters.PageNumber > 1 && count < filterParameters.PageNumber * filterParameters.PageSize)
                return CreateResult(await query.Skip(count - filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync());

            return CreateResult(await query.Skip((filterParameters.PageNumber - 1) * filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync());
        }

        //protected async Task<PageResult<TModel>> TakePage<T>(IQueryable<TModel> query, FilterBase filterParameters,
        //                                                     Expression<Func<TModel, T>> orderBy = null, params Expression<Func<TModel, object>>[] includes)
        //{
        //    query = query.Includes(includes);
        //    query = orderBy == null
        //        ? query.OrderBy(x => x.Id)
        //        : query.OrderBy(orderBy);

        //    var count = query.Count();

        //    PageResult<TModel> CreateResult(List<TModel> collection) => PageResult<TModel>.Ok(count, collection, filterParameters.PageNumber, filterParameters.PageSize);

        //    if (filterParameters.PageNumber > 1 && count < filterParameters.PageNumber * filterParameters.PageSize)
        //        return CreateResult(await query.Skip(count - filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync());

        //    return CreateResult(await query.Skip((filterParameters.PageNumber - 1) * filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync());
        //}

        protected async Task<TModel> UpdateBase(TModel itemToUpdate, DbSet<TModel> dbSet, HireRightDbContext context, params Expression<Func<TModel, object>>[] includes)
        {
            var errors = new List<ValidationResult>();
            if (itemToUpdate == null)
            {
                errors.Add(new ValidationResult(nameof(TModel) + " must not be null!"));
                throw new ApplicationException(GetConcatenatedErrorMessage(errors));
            }

            await CheckForValidItem(dbSet, itemToUpdate.Id, includes).ConfigureAwait(false);
            dbSet.AddOrUpdate(itemToUpdate);
            await context.SaveChangesAsync();
            return itemToUpdate;
        }

        private static string GetConcatenatedErrorMessage(List<ValidationResult> errors, string summaryMessage = "The following errors were encountered: ")
        {
            if (summaryMessage == null)
                summaryMessage = "The following errors were encounterd: ";

            var errorMessages = new List<string>() { summaryMessage };
            errorMessages.AddRange(errors.Select(x => x.ErrorMessage).ToList());

            return string.Join(Environment.NewLine, errorMessages.ToArray());
        }

        private async Task<TModel> CheckForValidItem(IQueryable<TModel> query, long itemGuid, params Expression<Func<TModel, object>>[] includes)
        {
            TModel item = await query.Includes(includes).FirstOrDefaultAsync(x => x.Id == itemGuid).ConfigureAwait(false);
            if (item == null)
                throw new ApplicationException(nameof(TModel) + " was not found!");
            return item;
        }

        private async Task<TModel> CheckForValidItem(IQueryable<TModel> query, Guid itemGuid, params Expression<Func<TModel, object>>[] includes)
        {
            TModel item = await query.Includes(includes).FirstOrDefaultAsync(x => x.RowGuid == itemGuid).ConfigureAwait(false);
            if (item == null)
                throw new ApplicationException(nameof(TModel) + " was not found!");
            return item;
        }
    }

    public static class QueryableExtensions
    {
        public static IQueryable<TModel> Includes<TModel>(this IQueryable<TModel> query, params Expression<Func<TModel, object>>[] includes)
        {
            foreach (Expression<Func<TModel, object>> expression in includes)
                query = query.Include(expression);
            return query;
        }
    }
}