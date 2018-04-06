using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.Persistence.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface ICategoriesBusinessLogic : IBusinessLogicBase<ScaleCategory, CategoryDTO>
    {
        Task<CategoryDTO> Add(CategoryDTO categoryDto);

        Task<CategoryDTO> Get(Guid categoryGuid);

        Task<PagingResultDTO<CategoryDTO>> Get(CategoryFilter filter);

        Task<CategoryDTO> Update(CategoryDTO categoryDto);

        Task<ICollection<CategoryDTO>> GetAll();
    }
}