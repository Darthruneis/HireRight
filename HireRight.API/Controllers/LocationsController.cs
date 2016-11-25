using DataTransferObjects.Data_Transfer_Objects;
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
    [RoutePrefix("api/Locations")]
    public class LocationsController : ApiControllerBase<LocationDTO>
    {
        private readonly Func<LocationFilter, Task<List<LocationDTO>>> _getPage;
        private readonly ILocationsBusinessLogic _locationsBusinessLogic;

        public LocationsController(ILocationsBusinessLogic bll)
        {
            _locationsBusinessLogic = bll;

            _add = _locationsBusinessLogic.Add;
            _update = _locationsBusinessLogic.Update;
            _getSingle = _locationsBusinessLogic.Get;
            _getPage = _locationsBusinessLogic.Get;
        }

        [HttpPost]
        public async Task<HttpResponseMessage> AddLocation(LocationDTO locationToAdd)
        {
            return await AddBase(locationToAdd);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetLocation(Guid locationGuid)
        {
            return await GetSingleBase(locationGuid);
        }

        [HttpGet]
        public async Task<HttpResponseMessage> GetLocations(LocationFilter filter)
        {
            return await GetMultipleBase(_getPage(filter));
        }

        [HttpPut]
        public async Task<HttpResponseMessage> UpdateLocation(LocationDTO locationToUpdate)
        {
            return await UpdateBase(locationToUpdate);
        }
    }
}