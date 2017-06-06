using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HireRight.Repository.Concrete
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

        public async Task<List<ScaleCategory>> Get(CategoryFilter filter)
        {
            using (HireRightDbContext context = ContextFunc.Invoke())
            {
                IQueryable<ScaleCategory> query = context.Categories;

                if (!string.IsNullOrWhiteSpace(filter.TitleFilter))
                    query = query.Where(x => x.Title.ToLower().Contains(filter.TitleFilter.ToLower()));

                if (!string.IsNullOrWhiteSpace(filter.DescriptionFilter))
                    query = query.Where(x => x.Description.ToLower().Contains(filter.DescriptionFilter.ToLower()));

                if (filter.ItemGuids.Any())
                    query = query.Where(x => filter.ItemGuids.Contains(x.Id));

                List<ScaleCategory> categoriesFound = await TakePage(query, filter);
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
    }
}