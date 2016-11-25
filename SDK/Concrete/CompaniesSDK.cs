using DataTransferObjects.Data_Transfer_Objects;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Concrete
{
    public class CompaniesSDK : ICompaniesSDK
    {
        private readonly IApiSDKClient<CompanyDTO> _client;

        public CompaniesSDK(IApiSDKClient<CompanyDTO> client)
        {
            _client = client;
            _client.BaseAddress = new Uri(ConfigurationConstants.ApiUrl + typeof(CompaniesSDK).Name.Replace("SDK", string.Empty));
        }

        public async Task<CompanyDTO> AddCompany(CompanyDTO companyToAdd)
        {
            ApiResponse<CompanyDTO> response = await _client.PostAsync(companyToAdd);

            return response.Results.First();
        }

        public async Task<List<CompanyDTO>> GetCompanies(CompanyFilter filter)
        {
            ApiResponse<CompanyDTO> response = await _client.GetAsync(filter.CreateQuery());

            return response.Results.ToList();
        }

        public async Task<CompanyDTO> GetCompany(Guid companyGuid)
        {
            ApiResponse<CompanyDTO> response = await _client.GetAsync($"?{nameof(companyGuid)}={companyGuid}");

            return response.Results.First();
        }

        public async Task<CompanyDTO> UpdateCompany(CompanyDTO companyToUpdate)
        {
            ApiResponse<CompanyDTO> response = await _client.PutAsync(companyToUpdate);

            return response.Results.First();
        }
    }
}