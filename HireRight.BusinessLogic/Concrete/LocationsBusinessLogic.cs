using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using HireRight.BusinessLogic.Abstract;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace HireRight.BusinessLogic.Concrete
{
    public class LocationsBusinessLogic : ILocationsBusinessLogic
    {
        private readonly ILocationsRepository _locationsRepository;

        public LocationsBusinessLogic(ILocationsRepository repo)
        {
            _locationsRepository = repo;
        }

        public async Task<LocationDTO> Add(LocationDTO locationDto)
        {
            CompanyLocation locationToAdd = ConvertDtoToModel(locationDto);

            return ConvertModelToDto(await _locationsRepository.Add(locationToAdd).ConfigureAwait(false));
        }

        public CompanyLocation ConvertDtoToModel(LocationDTO dto)
        {
            throw new NotImplementedException();
        }

        public LocationDTO ConvertModelToDto(CompanyLocation model)
        {
            throw new NotImplementedException();
        }

        public async Task<LocationDTO> Get(Guid locationGuid)
        {
            CompanyLocation location = await _locationsRepository.Get(locationGuid).ConfigureAwait(false);

            return ConvertModelToDto(location);
        }

        public async Task<List<LocationDTO>> Get(LocationFilter filter)
        {
            List<CompanyLocation> locations = await _locationsRepository.Get(filter).ConfigureAwait(false);

            return locations.Select(ConvertModelToDto).ToList();
        }

        public async Task<LocationDTO> Update(LocationDTO locationDto)
        {
            CompanyLocation locationToUpdate = ConvertDtoToModel(locationDto);

            return ConvertModelToDto(await _locationsRepository.Update(locationToUpdate).ConfigureAwait(false));
        }
    }
}