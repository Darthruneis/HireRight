using HireRight.BusinessLogic.Abstract;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Extensions;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class CategoriesBusinessLogic : ICategoriesBusinessLogic
    {
        private readonly ICategoriesRepository _categoriesRepository;

        public CategoriesBusinessLogic(ICategoriesRepository repository)
        {
            _categoriesRepository = repository;
        }

        public async Task<CategoryDTO> Add(CategoryDTO categoryDto)
        {
            ScaleCategory categoryToAdd = ConvertDtoToModel(categoryDto);

            return ConvertModelToDto(await _categoriesRepository.Add(categoryToAdd));
        }

        public ScaleCategory ConvertDtoToModel(CategoryDTO dto)
        {
            ScaleCategory model = new ScaleCategory(dto.Title, dto.Description);
            model.RowGuid = dto.RowGuid;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public CategoryDTO ConvertModelToDto(ScaleCategory model)
        {
            CategoryDTO dto = new CategoryDTO(model.Title, model.Description);
            dto.RowGuid = model.RowGuid;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Industries = model.IndustryBinders?.Select(x => x.IndustryId).ToList() ?? new List<long>();

            return dto;
        }

        public async Task<CategoryDTO> Get(Guid categoryGuid)
        {
            ScaleCategory category = await _categoriesRepository.Get(categoryGuid);

            return ConvertModelToDto(category);
        }

        public async Task<PagingResultDTO<CategoryDTO>> Get(CategoryFilter filter)
        {
            PageResult<ScaleCategory> categories = await _categoriesRepository.Get(filter);

            return categories.PageResultToDto(ConvertModelToDto);
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDto)
        {
            ScaleCategory categoryToUpdate = ConvertDtoToModel(categoryDto);

            return ConvertModelToDto(await _categoriesRepository.Update(categoryToUpdate));
        }

        public async Task<ICollection<CategoryDTO>> GetAll()
        {
            var categories = await _categoriesRepository.GetAll();
            categories.ToList().ForEach(x => x.IndustryBinders = x.IndustryBinders.Where(y => y.IsActive).ToList());

            return categories.Select(ConvertModelToDto).ToList();
        }
    }
}