﻿using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

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
                return await _repositoryBase.AddBase(itemToAdd, context.Categories, context);
            }
        }

        public async Task<List<ScaleCategory>> Get(CategoryFilter filter)
        {
            List<ScaleCategory> categoriesFound;

            using (HireRightDbContext context = new HireRightDbContext())
            {
                IQueryable<ScaleCategory> query = context.Categories;

                if (!string.IsNullOrWhiteSpace(filter.TitleFilter))
                    query = query.Where(x => x.Title.ToLower().Contains(filter.TitleFilter.ToLower()));

                if (!string.IsNullOrWhiteSpace(filter.DescriptionFilter))
                    query = query.Where(x => x.Description.ToLower().Contains(filter.DescriptionFilter.ToLower()));

                if (filter.ItemGuids.Any())
                    query = query.Where(x => filter.ItemGuids.Contains(x.Id));

                categoriesFound = await _repositoryBase.TakePage(query, filter);
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
                return await _repositoryBase.UpdateBase(itemToUpdate, context.Categories, context);
            }
        }
    }
}