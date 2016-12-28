using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SDK.Abstract
{
    public interface ICategoriesSDK
    {
        Task<CategoryDTO> AddCategory(CategoryDTO categoryToAdd);

        Task<List<CategoryDTO>> GetCategories();

        Task<CategoryDTO> GetCategory(Guid categoryGuid);

        Task<CategoryDTO> UpdateCategory(CategoryDTO categoryToUpdate);
    }
}