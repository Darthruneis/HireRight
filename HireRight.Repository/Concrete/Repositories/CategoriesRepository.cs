using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace HireRight.Repository.Concrete
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly IRepositoryBase<ScaleCategory> _repositoryBase;

        public CategoriesRepository(IRepositoryBase<ScaleCategory> repositoryBase)
        {
            _repositoryBase = repositoryBase;
        }

        public async Task<ScaleCategory> Add(ScaleCategory itemToAdd)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.AddBase(itemToAdd, context.Categories);
            }
        }

        public async Task<List<ScaleCategory>> Get()
        {
            List<ScaleCategory> categoriesFound;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<ScaleCategory> query = context.Categories;

                categoriesFound = await query.ToListAsync();
            }

            return categoriesFound;
        }

        public async Task<ScaleCategory> Get(Guid itemGuid)
        {
            ScaleCategory category;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                category = await _repositoryBase.GetBase(itemGuid, context.Categories);
            }

            return category;
        }

        public async Task<ScaleCategory> Update(ScaleCategory itemToUpdate)
        {
            using (HireRightDbContext context = new HireRightDbContext())
            {
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Categories);
            }
        }
    }
}