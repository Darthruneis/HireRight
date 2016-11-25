using DataTransferObjects.Data_Transfer_Objects;
using DataTransferObjects.Filters;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DataTransferObjects.Filters.Concrete;

namespace SDK.Abstract
{
    public interface ILocationsSDK
    {
        Task<LocationDTO> AddLocation(LocationDTO locationToAdd);

        Task<LocationDTO> GetLocation(Guid locationGuid);

        Task<List<LocationDTO>> GetLocations(LocationFilter filter);

        Task<LocationDTO> UpdateLocation(LocationDTO locationToUpdate);
    }
}