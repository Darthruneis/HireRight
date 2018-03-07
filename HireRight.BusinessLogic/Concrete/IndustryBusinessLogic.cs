﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Data_Transfer_Objects;
using HireRight.BusinessLogic.Abstract;
using HireRight.Repository.Abstract;

namespace HireRight.BusinessLogic.Concrete
{
    public class IndustryBusinessLogic : IIndustryBusinessLogic
    {
        private readonly IIndustryRepository _industryRepository;

        public IndustryBusinessLogic(IIndustryRepository industryRepository)
        {
            _industryRepository = industryRepository;
        }

        public async Task<ICollection<IndustryDTO>> GetAll()
        {
            var industries = await _industryRepository.GetAll();
            return industries.Select(x => new IndustryDTO(x.Name, x.Id)).ToList();
        }
    }
}