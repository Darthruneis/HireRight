﻿using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters.Concrete;
using HireRight.BusinessLogic.Abstract;
using HireRight.BusinessLogic.Extensions;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Concrete
{
    public class CompanyBusinessLogic : ICompanyBusinessLogic
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IOrdersBusinessLogic _ordersBusinessLogic;

        public CompanyBusinessLogic(ICompanyRepository repo, IOrdersBusinessLogic bll)
        {
            _companyRepository = repo;
            _ordersBusinessLogic = bll;
        }

        public async Task<CompanyDTO> Add(CompanyDTO companyDto)
        {
            Company companyToAdd = ConvertDtoToModel(companyDto);

            CompanyDTO dto = ConvertModelToDto(await _companyRepository.Add(companyToAdd).ConfigureAwait(false));

            NewOrderDTO order = new NewOrderDTO();
            order.Order = companyDto.Orders.First();
            order.Company = dto;
            order.PrimaryContact = dto.Contacts.First(x => x.IsPrimary);
            order.SecondaryContact = dto.Contacts.First(x => !x.IsPrimary);
            order.CreatedUtc = dto.CreatedUtc;
            order.Id = dto.Orders.First().Id;

            await _ordersBusinessLogic.CreateOrder(order);

            return dto;
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
            dto.Contacts = model.Contacts.Select(x => x.ConvertModelToDto()).ToList();
            dto.Notes = model.Notes;
            dto.Locations = model.Locations.Select(x => x.ConvertModelToDto()).ToList();
            dto.Orders = model.Orders.Select(x => x.ConvertModelToDto()).ToList();

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