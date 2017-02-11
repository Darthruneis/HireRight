using DataTransferObjects;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using SDK.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SDK.Concrete
{
    public class CompaniesSDK : ICompaniesSDK
    {
        private readonly IApiSDKClient _client;

        public CompaniesSDK(IApiSDKClient client)
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
            string query = filter.CreateQuery();
            ApiResponse<CompanyDTO> response = await _client.GetAsync<CompanyDTO>(query);

            return response.Results.ToList();
        }

        public async Task<CompanyDTO> GetCompany(Guid companyGuid)
        {
            ApiResponse<CompanyDTO> response = await _client.GetAsync<CompanyDTO>($"?{nameof(companyGuid)}={companyGuid}");

            return response.Results.First();
        }

        public async Task<CompanyDTO> UpdateCompany(CompanyDTO companyToUpdate)
        {
            ApiResponse<CompanyDTO> response = await _client.PutAsync(companyToUpdate);

            return response.Results.First();
        }
    }
}