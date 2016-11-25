using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface ICompaniesSDK
    {
        Task<CompanyDTO> AddCompany(CompanyDTO companyToAdd);

        Task<List<CompanyDTO>> GetCompanies(CompanyFilter filter);

        Task<CompanyDTO> GetCompany(Guid companyGuid);

        Task<CompanyDTO> UpdateCompany(CompanyDTO companyToUpdate);
    }
}