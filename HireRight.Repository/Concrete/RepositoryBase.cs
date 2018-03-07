using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.Repository.Concrete
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

            if (result.Id == Guid.Empty)
                errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " - Id was not updated."));

            if (result.CreatedUtc == default(DateTime))
                errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " CreatedUtc was not updated."));

            return await Task.FromResult(result).ConfigureAwait(false);
        }

        protected async Task<TModel> GetBase(Guid itemGuid, IQueryable<TModel> query, params Expression<Func<TModel, object>>[] includes)
        {
            if (itemGuid == Guid.Empty)
                throw new InvalidOperationException("An empty guid cannot be found in the repository!");

            TModel item = await CheckForValidItem(query, itemGuid, includes).ConfigureAwait(false);
            return item;
        }

        protected Task<PageResult<TModel>> TakePage(IQueryable<TModel> query, FilterBase filterParameters, params Expression<Func<TModel, object>>[] includes)
            => TakePage(query, filterParameters, x => x.Id, includes);

        protected async Task<PageResult<TModel>> TakePage<T>(IQueryable<TModel> query, FilterBase filterParameters,
                                                             Expression<Func<TModel, T>> orderBy = null, params Expression<Func<TModel, object>>[] includes)
        {
            query = query.Includes(includes);
            query = orderBy == null
                ? query.OrderBy(x => x.Id)
                : query.OrderBy(orderBy);

            var count = query.Count();

            PageResult<TModel> CreateResult(List<TModel> collection) => PageResult<TModel>.Ok(count, collection, filterParameters.PageNumber, filterParameters.PageSize);

            if (filterParameters.PageNumber > 1 && count < filterParameters.PageNumber * filterParameters.PageSize)
                return CreateResult(await query.Skip(count - filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync());

            return CreateResult(await query.Skip((filterParameters.PageNumber - 1) * filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync());
        }

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

        private async Task<TModel> CheckForValidItem(IQueryable<TModel> query, Guid itemGuid, params Expression<Func<TModel, object>>[] includes)
        {
            TModel item = await query.Includes(includes).FirstOrDefaultAsync(x => x.Id == itemGuid).ConfigureAwait(false);
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