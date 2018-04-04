using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Data_Transfer_Objects;

namespace HireRight.BusinessLogic.Abstract
{
    public interface IIndustryBusinessLogic
    {
        Task<ICollection<IndustryDTO>> GetAll();

        Task<ICollection<IndustryWithAssessmentsDto>> GetAllWithAssessments();
    }
}