using HireRight.EntityFramework.CodeFirst.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Abstract;
using HireRight.EntityFramework.CodeFirst.Database_Context;

namespace HireRight.Repository.Abstract
{
    public interface IRepositoryBase<TModel> where TModel : PocoBase
    {
        Task<TModel> AddBase(TModel itemToAdd, DbSet<TModel> dbSet, HireRightDbContext context);

        Task<TModel> GetBase(Guid itemGuid, IQueryable<TModel> query);

        Task<List<TModel>> TakePage(IQueryable<TModel> query, FilterBase filterParameters);

        Task<TModel> UpdateBase(TModel itemToUpdate, DbSet<TModel> dbSet, HireRightDbContext context);
    }
}