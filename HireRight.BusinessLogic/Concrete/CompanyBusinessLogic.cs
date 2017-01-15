using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Extensions;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class CompanyBusinessLogic : ICompanyBusinessLogic
    {
        private readonly ICompanyRepository _companyRepository;

        public CompanyBusinessLogic(ICompanyRepository repo)
        {
            _companyRepository = repo;
        }

        public async Task<CompanyDTO> Add(CompanyDTO companyDto)
        {
            Company companyToAdd = ConvertDtoToModel(companyDto);

            return ConvertModelToDto(await _companyRepository.Add(companyToAdd).ConfigureAwait(false));
        }

        public Company ConvertDtoToModel(CompanyDTO dto)
        {
            Company model = new Company();
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;
            model.Name = dto.Name;
            model.Address = dto.BillingAddress.ConvertDtoToModel();
            model.Notes = dto.Notes;

            foreach (OrderDetailsDTO orderDetailsDTO in dto.Orders)
                model.Orders.Add(orderDetailsDTO.ConvertDtoToModel());

            foreach (LocationDTO locationDTO in dto.Locations)
                model.Locations.Add(locationDTO.ConvertDtoToModel());

            foreach (ContactDTO contactDTO in dto.Contacts)
                model.Contacts.Add(contactDTO.ConvertDtoToModel());

            return model;
        }

        public CompanyDTO ConvertModelToDto(Company model)
        {
            CompanyDTO dto = new CompanyDTO();
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;
            dto.Name = model.Name;
            dto.BillingAddress = model.Address.ConvertModelToDto();

            return dto;
        }

        public async Task<CompanyDTO> Get(Guid companyGuid)
        {
            Company company = await _companyRepository.Get(companyGuid).ConfigureAwait(false);

            return ConvertModelToDto(company);
        }

        public async Task<List<CompanyDTO>> Get(CompanyFilter filter)
        {
            List<Company> companies = await _companyRepository.Get(filter).ConfigureAwait(false);

            return companies.Select(ConvertModelToDto).ToList();
        }

        public async Task<CompanyDTO> Update(CompanyDTO companyDto)
        {
            Company companyToUpdate = ConvertDtoToModel(companyDto);

            return ConvertModelToDto(await _companyRepository.Update(companyToUpdate).ConfigureAwait(false));
        }
    }
}