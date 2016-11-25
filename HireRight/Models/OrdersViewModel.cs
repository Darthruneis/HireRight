using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace HireRight.Models
{
    public class OrdersViewModel
    {
        [Display(Name = "Notes (optional)")]
        public string Notes { get; set; }

        [HiddenInput(DisplayValue = false)]
        public List<string> Positions { get; set; }

        [Required]
        [Display(Name = "Positions to fill (separated by commas)")]
        public string PositionsToFill { get; set; }

        [Display(Name = "Product")]
        public List<ProductDTO> Products { get; }

        [Required]
        [Range(1, 100000, ErrorMessage = "Order Quantity must be between 1 and 100,000.")]
        public uint Quantity { get; set; }

        public ProductDTO SelectedProduct { get; set; }
        public Guid SelectedProductId { get; set; }

        public OrdersViewModel(params ProductDTO[] products)
        {
            Products = products.ToList();
        }

        public OrderDetailsDTO ConvertToOrderDetailsDTO()
        {
            OrderDetailsDTO dto = new OrderDetailsDTO();

            dto.Notes = Notes;
            dto.Quantity = (int)Quantity;
            dto.PositionsOfInterest = Positions;
            dto.ProductId = SelectedProductId;

            return dto;
        }
    }
}