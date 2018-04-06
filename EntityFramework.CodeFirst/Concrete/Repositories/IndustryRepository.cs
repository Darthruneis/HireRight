using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using HireRight.Persistence.Abstract;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Concrete.Repositories
{
    public class IndustryRepository : RepositoryWithContextFunc, IIndustryRepository
    {
        public IndustryRepository() { }

        public async Task<ICollection<Industry>> GetAll()
        {
            using (var context = ContextFunc())
                return await context.Industries.Where(x => x.IsActive).Include(x => x.CategoryBinders).ToListAsync();
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