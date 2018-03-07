using System.Collections.Generic;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.Repository.Abstract
{
    public interface IIndustryRepository
    {
        Task<ICollection<Industry>> GetAll();
    }
}