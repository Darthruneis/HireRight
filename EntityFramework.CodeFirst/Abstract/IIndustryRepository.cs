using System.Collections.Generic;
using System.Threading.Tasks;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.Persistence.Abstract
{
    public interface IIndustryRepository
    {
        Task<ICollection<Industry>> GetAll();

        Task<ICollection<Industry>> GetAllWithAssessments();
    }
}