﻿using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HireRight.BusinessLogic.Abstract
{
    public interface ICategoriesBusinessLogic : IBusinessLogicBase<ScaleCategory, CategoryDTO>
    {
        Task<CategoryDTO> Add(CategoryDTO categoryDto);

        Task<CategoryDTO> Get(Guid categoryGuid);

        Task<List<CategoryDTO>> Get();

        Task<CategoryDTO> Update(CategoryDTO categoryDto);
    }
}