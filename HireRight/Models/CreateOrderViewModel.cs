using DataTransferObjects.Data_Transfer_Objects;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HireRight.Models
{
    public class CreateOrderViewModel
    {
        public AdminViewModel Admin { get; set; }
        public CompanyViewModel Company { get; set; }
        public OrdersViewModel Order { get; set; }
        public PrimaryContactViewModel Primary { get; set; }
        public Guid ProductSelected { get; set; }

        public CreateOrderViewModel(params ProductDTO[] products) : this()
        {
            Order = new OrdersViewModel(products);
        }

        public CreateOrderViewModel()
        {
            Company = new CompanyViewModel();
            Primary = new PrimaryContactViewModel();
            Admin = new AdminViewModel();
            Order = new OrdersViewModel();
        }

        public NewOrderDTO ConvertToNewOrderDTO()
        {
            NewOrderDTO dto = new NewOrderDTO();

            dto.PrimaryContact = Primary.ConvertToContactDTO();
            dto.SecondaryContact = Admin.ConvertToContactDTO();
            dto.Order = Order.ConvertToOrderDetailsDTO();
            dto.Company = Company.ConvertToCompanyDTO();
            dto.Company.Orders.Add(dto.Order);
            dto.SecondaryContact.Address = dto.Company.BillingAddress;

            if (dto.SecondaryContact.FullName == null)
                dto.PrimaryContact.IsAdmin = true;
            else
                dto.SecondaryContact.IsAdmin = true;

            dto.PrimaryContact.IsPrimary = true;
            dto.SecondaryContact.OfficeNumber = dto.SecondaryContact.CellNumber;

            dto.Company.Contacts.Add(dto.PrimaryContact);
            dto.Company.Contacts.Add(dto.SecondaryContact);

            return dto;
        }
    }
}