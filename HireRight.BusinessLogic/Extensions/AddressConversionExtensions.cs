using DataTransferObjects.Data_Transfer_Objects;
using HireRight.Persistence.Models.CompanyAggregate;

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
            return new AddressDTO
                   {
                       City = model.City,
                       Country = model.Country,
                       PostalCode = model.PostalCode,
                       State = model.State,
                       StreetAddress = model.StreetAddress,
                       UnitNumber = model.UnitNumber
                   };
        }
    }
}