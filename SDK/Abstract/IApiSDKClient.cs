using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Threading.Tasks;
using DataTransferObjects;

namespace SDK.Abstract
{
    public interface IApiSDKClient<TDto> where TDto : DataTransferObjectBase
    {
        Uri BaseAddress { get; set; }

        Task<ApiResponse<TDto>> GetAsync(string query);

        Task<ApiResponse<TDto>> PostAsync(TDto content);

        Task<ApiResponse<TDto>> PutAsync(TDto content);
    }
}