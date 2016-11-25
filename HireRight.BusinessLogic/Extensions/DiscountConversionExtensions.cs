﻿using DataTransferObjects.Data_Transfer_Objects;
using HireRight.EntityFramework.CodeFirst.Models;

namespace HireRight.BusinessLogic.Extensions
{
    public static class DiscountConversionExtensions
    {
        public static Discount ConvertDtoToModel(this DiscountDTO dto)
        {
            Discount model = new Discount(dto.IsPercent, dto.Amount, dto.Threshold);
            model.Id = dto.Id;
            model.ProductId = dto.ProductId;
            model.CreatedUtc = dto.CreatedUtc;

            return model;
        }

        public static DiscountDTO ConvertModelToDto(this Discount model)
        {
            DiscountDTO dto = new DiscountDTO();
            dto.Id = model.Id;
            dto.CreatedUtc = model.CreatedUtc;
            dto.ProductId = model.ProductId;
            dto.Amount = model.Amount;
            dto.IsPercent = model.IsPercent;
            dto.Threshold = model.Threshold;

            return dto;
        }
    }
}