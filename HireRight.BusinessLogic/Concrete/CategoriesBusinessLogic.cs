using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

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
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public CategoryDTO ConvertModelToDto(ScaleCategory model)
        {
            CategoryDTO dto = new CategoryDTO(model.Title, model.Description);
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;

            return dto;
        }

        public async Task<CategoryDTO> Get(Guid categoryGuid)
        {
            ScaleCategory category = await _categoriesRepository.Get(categoryGuid);

            return ConvertModelToDto(category);
        }

        public async Task<List<CategoryDTO>> Get()
        {
            List<ScaleCategory> categories = await _categoriesRepository.Get();

            return categories.Select(ConvertModelToDto).ToList();
        }

        public async Task<CategoryDTO> Update(CategoryDTO categoryDto)
        {
            ScaleCategory categoryToUpdate = ConvertDtoToModel(categoryDto);

            return ConvertModelToDto(await _categoriesRepository.Update(categoryToUpdate));
        }
    }
}