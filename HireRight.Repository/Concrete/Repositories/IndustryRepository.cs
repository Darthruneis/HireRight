using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Database_Context;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;
using System.Data.Entity;
using System.Linq;
using HireRight.Repository.Abstract;

namespace HireRight.Repository.Concrete
{
    public class IndustryRepository : RepositoryWithContextFunc, IIndustryRepository
    {
        public IndustryRepository() { }

        public async Task<ICollection<Industry>> GetAll()
        {
            using (var context = ContextFunc())
                return await context.Industries.Include(x => x.CategoryBinders).ToListAsync();
        }

        public async Task<ICollection<Industry>> GetAllWithAssessments()
        {
            using (var context = ContextFunc())
            {
                return await context.Industries.Where(x => x.IsActive).Include(x => x.Assessments.Select(y => y.Assessment)).ToListAsync();
            }
        }
    }
}