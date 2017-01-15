using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models.OrderAggregate;

namespace HireRight.BusinessLogic.Extensions
{
    public static class OrderConversionExtensions
    {
        public static Order ConvertDtoToModel(this OrderDetailsDTO dto)
        {
            Order model = new Order();

            model.Notes = dto.NotesAndPositions.Notes;
            model.PositionsOfInterest = dto.NotesAndPositions.PositionsOfInterest;
            model.ProductId = dto.ProductId;
            model.Quantity = dto.Quantity;
            model.Status = OrderStatus.Pending;
            model.Id = dto.Id;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public static OrderDetailsDTO ConvertModelToDto(this Order model)
        {
            OrderDetailsDTO dto = new OrderDetailsDTO();
            dto.NotesAndPositions.Notes = model.Notes;
            dto.NotesAndPositions.PositionsOfInterest = model.PositionsOfInterest;
            dto.ProductId = model.ProductId;
            dto.Quantity = model.Quantity;
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;

            return dto;
        }
    }
}