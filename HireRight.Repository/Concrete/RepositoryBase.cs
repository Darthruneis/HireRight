using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;

namespace HireRight.Repository.Concrete
{
    public abstract class RepositoryBase<TModel>
        where TModel : PocoBase
    {
        protected Func<HireRightDbContext> ContextFunc;

        protected RepositoryBase(Func<HireRightDbContext> contextFunc)
        {
            ContextFunc = contextFunc;
        }

        public async Task<TModel> AddBase(TModel itemToAdd, DbSet<TModel> dbSet, HireRightDbContext context)
        {
            var errors = new List<ValidationResult>();
            if (itemToAdd == null)
            {
                errors.Add(new ValidationResult(nameof(TModel) + " must not be null!"));
                throw new ApplicationException(GetConcatenatedErrorMessage(errors));
            }

            try
            {
                TModel result = dbSet.Add(itemToAdd);
                await context.SaveChangesAsync();

                if (result.Id == Guid.Empty)
                    errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " - Id was not updated."));

                if (result.CreatedUtc == default(DateTime))
                    errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " CreatedUtc was not updated."));

                return await Task.FromResult(result).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationResult(ex.Message));
                throw new ApplicationException(GetConcatenatedErrorMessage(errors));
            }
        }

        public async Task<TModel> GetBase(Guid itemGuid, IQueryable<TModel> query)
        {
            if (itemGuid == Guid.Empty)
                throw new InvalidOperationException("An empty guid cannot be found in the repository!");

            TModel item = await CheckForValidItem(query, itemGuid).ConfigureAwait(false);
            return item;
        }

        public async Task<List<TModel>> TakePage(IQueryable<TModel> query, FilterBase filterParameters)
        {
            query = query.OrderBy(x => x.Id);
            var count = query.Count();
            if (filterParameters.PageNumber > 1 && count < filterParameters.PageNumber * filterParameters.PageSize)
                return await query.Skip(count - filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync();
            return await query.Skip((filterParameters.PageNumber - 1) * filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync();
        }

        public async Task<TModel> UpdateBase(TModel itemToUpdate, DbSet<TModel> dbSet, HireRightDbContext context)
        {
            var errors = new List<ValidationResult>();
            if (itemToUpdate == null)
            {
                errors.Add(new ValidationResult(nameof(TModel) + " must not be null!"));
                throw new ApplicationException(GetConcatenatedErrorMessage(errors));
            }

            try
            {
                await CheckForValidItem(dbSet, itemToUpdate.Id).ConfigureAwait(false);
                dbSet.AddOrUpdate(itemToUpdate);
                await context.SaveChangesAsync();
                return itemToUpdate;
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationResult(ex.Message));
                throw new ApplicationException(GetConcatenatedErrorMessage(errors));
            }
        }

        private static string GetConcatenatedErrorMessage(List<ValidationResult> errors, string summaryMessage = "The following errors were encounterd: ")
        {
            if (summaryMessage == null)
                summaryMessage = "The following errors were encounterd: ";

            List<string> errorMessages = errors.Select(x => x.ErrorMessage).ToList();
            return string.Join(Environment.NewLine, summaryMessage, errorMessages);
        }

        private async Task<TModel> CheckForValidItem(IQueryable<TModel> query, Guid itemGuid)
        {
            TModel item = await query.FirstOrDefaultAsync(x => x.Id == itemGuid).ConfigureAwait(false);
            if (item == null)
                throw new ApplicationException(nameof(TModel) + " was not found!");
            return item;
        }
    }
}