﻿using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.BusinessLogic.Abstract;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.API.Controllers
{
    [RoutePrefix("api/Companies")]
    public class CompaniesController : ApiControllerBase<CompanyDTO>
    {
        private readonly ICompanyBusinessLogic _companyBusinessLogic;

        private readonly Func<CompanyFilter, Task<List<CompanyDTO>>> _getPage;

        public CompaniesController(ICompanyBusinessLogic bll)
        {
            _companyBusinessLogic = bll;

            _add = _companyBusinessLogic.Add;
            _update = _companyBusinessLogic.Update;
            _getSingle = _companyBusinessLogic.Get;
            _getPage = _companyBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddCompany(CompanyDTO companyToAdd)
        {
            return await AddBase(companyToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCompanies(CompanyFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetCompany(Guid companyGuid)
        {
            return await GetSingleBase(companyGuid);
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateAccount(CompanyDTO companyToUpdate)
        {
            return await UpdateBase(companyToUpdate);
        }
    }
}