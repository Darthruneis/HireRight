using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models;
using HireRight.EntityFramework.CodeFirst.Models.CompanyAggregate;

namespace HireRight.BusinessLogic.Extensions
{
    public static class AddressConversionExtensions
    {
        public static Address ConvertDtoToModel(this AddressDTO dto)
        {
            return new Address(dto.Country, dto.StreetAddress, dto.City, dto.State, dto.PostalCode, dto.UnitNumber);
        }

        public static AddressDTO ConvertModelToDto(this Address model)
        {
            return new AddressDTO(model);
        }
    }
}