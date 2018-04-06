﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Database_Context;
using HireRight.Persistence.Models;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Concrete.Repositories
{
    public class CategoriesRepository : RepositoryBase<ScaleCategory>, ICategoriesRepository
    {
        public CategoriesRepository() : base(() => new HireRightDbContext())
        {
        }

        public CategoriesRepository(Func<HireRightDbContext> contextFunc) : base(contextFunc)
        {
        }

        public async Task<ScaleCategory> Add(ScaleCategory itemToAdd)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await AddBase(itemToAdd, context.Categories, context);
            }
        }

        public async Task<PageResult<ScaleCategory>> Get(CategoryFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<ScaleCategory> query = context.Categories;

                if (!string.IsNullOrWhiteSpace(filter.TitleFilter))
                    query = query.Where(x => x.Title.ToLower().Contains(filter.TitleFilter.ToLower()));

                if (!string.IsNullOrWhiteSpace(filter.DescriptionFilter))
                    query = query.Where(x => x.Description.ToLower().Contains(filter.DescriptionFilter.ToLower()));

                if (filter.ItemGuids.Any())
                    query = query.Where(x => filter.ItemGuids.Contains(x.RowGuid));

                var categoriesFound = await TakePage(query, filter, x => x.Title);
                return categoriesFound;
            }
        }

        public async Task<ScaleCategory> Get(Guid itemGuid)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                ScaleCategory category = await GetBase(itemGuid, context.Categories);
                return category;
            }
        }

        public async Task<ScaleCategory> Update(ScaleCategory itemToUpdate)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                return await UpdateBase(itemToUpdate, context.Categories, context);
            }
        }

        public async Task<ICollection<ScaleCategory>> GetAll()
        {
            using (var context = ContextFunc())
                return await context.Categories
                                    .Include(x => x.IndustryBinders)
                                    .Where(x => x.IsActive)
                                    .Where(x => context.IndustryScaleCategoryBinders.Any(y => y.IsActive && y.CategoryId == x.Id))
                                    .OrderBy(x => x.Title)
                                    .ToListAsync();
        }
    }
}