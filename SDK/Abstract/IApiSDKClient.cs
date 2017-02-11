using DataTransferObjects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDK.Abstract
{
    public interface IApiSDKClient
    {
        Uri BaseAddress { get; set; }

        Task<ApiResponse<TDto>> GetAsync<TDto>(string query);

        Task<ApiResponse<TDto>> PostAsync<TDto>(TDto content);

        Task<ApiResponse<TDto>> PostAsync<TDto>(IList<TDto> content);

        Task<ApiResponse<TDto>> PutAsync<TDto>(TDto content);
    }
}