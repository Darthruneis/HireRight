using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.BusinessLogic.Extensions
{
    public static class AddressConversionExtensions
    {
        public static Address ConvertDtoToModel(this AddressDTO dto)
        {
            Address model = new Address(dto.Country);
            model.City = dto.City;
            model.PostalCode = dto.PostalCode;
            model.State = dto.State;
            model.StreetAddress = dto.StreetAddress;
            model.UnitNumber = dto.UnitNumber;

            return model;
        }

        public static AddressDTO ConvertModelToDto(this Address model)
        {
            return new AddressDTO(model);
        }
    }
}