using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Abstract
{
    public interface ICompanyBusinessLogic : IBusinessLogicBase<Company, CompanyDTO>
    {
        Task<CompanyDTO> Add(CompanyDTO companyDto);

        Task<CompanyDTO> Get(Guid companyGuid);

        Task<PagingResultDTO<CompanyDTO>> Get(CompanyFilter filter);

        Task<CompanyDTO> Update(CompanyDTO companyDto);
    }
}