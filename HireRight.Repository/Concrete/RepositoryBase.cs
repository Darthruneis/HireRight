using DataTransferObjects.Filters;
using HireRight.EntityFramework.CodeFirst.Abstract;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Abstract;

namespace HireRight.Repository.Concrete
{
    public class RepositoryBase<TModel> : IRepositoryBase<TModel>
        where TModel : PocoBase
    {
        public async Task<TModel> AddBase(TModel itemToAdd, DbSet<TModel> dbSet)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (itemToAdd == null)
            {
                errors.Add(new ValidationResult(nameof(TModel) + " must not be null!"));
                ThrowConcatenatedErrorMessage(errors);
            }

            try
            {
                TModel result = dbSet.Add(itemToAdd);

                if (result.Id == Guid.Empty)
                    errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " - Id was not updated."));

                if (result.CreatedUtc == default(DateTime))
                    errors.Add(new ValidationResult("Failed to create " + nameof(TModel) + " CreatedUtc was not updated."));

                return await Task.FromResult(result).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationResult(ex.Message));
                ThrowConcatenatedErrorMessage(errors);
            }

            ThrowConcatenatedErrorMessage(errors);
            return null;
        }

        public async Task<TModel> GetBase(Guid itemGuid, IQueryable<TModel> query)
        {
            if (itemGuid == Guid.Empty)
                throw new InvalidOperationException("An empty guid cannot be found in the repository!");

            TModel item = await CheckForValidItem(query, itemGuid).ConfigureAwait(false);

            return item;
        }

        public async Task<List<TModel>> TakePage(IQueryable<TModel> query, Filter<TModel> filterParameters)
        {
            if (filterParameters.ItemGuids != null && filterParameters.ItemGuids.Any())
                query = query.Where(x => filterParameters.ItemGuids.Contains(x.Id));

            return await query.OrderBy(x => x.Id).Skip((filterParameters.PageNumber - 1) * filterParameters.PageSize).Take(filterParameters.PageSize).ToListAsync();
        }

        public async Task<TModel> UpdateBase(TModel itemToUpdate, DbSet<TModel> dbSet)
        {
            List<ValidationResult> errors = new List<ValidationResult>();

            if (itemToUpdate == null)
            {
                errors.Add(new ValidationResult(nameof(TModel) + " must not be null!"));
                ThrowConcatenatedErrorMessage(errors);
            }

            try
            {
                await CheckForValidItem(dbSet, itemToUpdate.Id).ConfigureAwait(false);

                dbSet.AddOrUpdate(itemToUpdate);
                return itemToUpdate;
            }
            catch (Exception ex)
            {
                errors.Add(new ValidationResult(ex.Message));
                ThrowConcatenatedErrorMessage(errors);
            }

            ThrowConcatenatedErrorMessage(errors);
            return null;
        }

        private static void ThrowConcatenatedErrorMessage(List<ValidationResult> errors, string summaryMessage = "The following errors were encounterd: ")
        {
            List<string> errorMessages = errors.Select(x => x.ErrorMessage).ToList();

            if (summaryMessage == null)
                summaryMessage = "The following errors were encounterd: ";

            throw new ApplicationException(string.Join(Environment.NewLine, summaryMessage, errorMessages));
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