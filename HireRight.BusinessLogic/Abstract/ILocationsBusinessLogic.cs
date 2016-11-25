using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.EntityFramework.CodeFirst.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.BusinessLogic.Abstract
{
    public interface ILocationsBusinessLogic : IBusinessLogicBase<CompanyLocation, LocationDTO>
    {
        Task<LocationDTO> Add(LocationDTO locationDto);

        Task<LocationDTO> Get(Guid locationGuid);

        Task<List<LocationDTO>> Get(LocationFilter filter);

        Task<LocationDTO> Update(LocationDTO locationDto);
    }
}